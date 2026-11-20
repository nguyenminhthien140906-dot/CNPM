
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[ApiController]
[Route("api/ai")]
[Authorize]
public class AiController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ApplicationDbContext _db;

    public AiController(
        IHttpClientFactory httpClientFactory,
        ApplicationDbContext db)
    {
        _httpClientFactory = httpClientFactory;
        _db = db;
    }

    private long GetUserId()
    {
        var value = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return long.TryParse(value, out var id) ? id : 0;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat(
        [FromBody] AiChatRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest(new
            {
                success = false,
                message = "Nội dung tin nhắn không được để trống."
            });
        }

        var userId = GetUserId();

        if (userId <= 0)
            return Unauthorized();

        AIConversation? conversation = null;

        if (request.ConversationId.HasValue)
        {
            conversation = await _db.AIConversations
                .FirstOrDefaultAsync(
                    x => x.ConversationId == request.ConversationId.Value
                      && x.UserId == userId,
                    cancellationToken);

            if (conversation == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Không tìm thấy cuộc hội thoại."
                });
            }
        }

        try
        {
            var client = _httpClientFactory.CreateClient("FlaskAI");

            using var response = await client.PostAsJsonAsync(
                "/api/chat",
                new { message = request.Message },
                cancellationToken);

            var result = await response.Content.ReadAsStringAsync(
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return new ContentResult
                {
                    StatusCode = (int)response.StatusCode,
                    ContentType = "application/json",
                    Content = result
                };
            }

            using var json = JsonDocument.Parse(result);

            var root = json.RootElement;

            if (!root.TryGetProperty("success", out var success)
                || success.ValueKind != JsonValueKind.True
                || !root.TryGetProperty("reply", out var replyValue)
                || replyValue.ValueKind != JsonValueKind.String)
            {
                return StatusCode(502, new
                {
                    success = false,
                    message = "Phản hồi Flask AI không hợp lệ."
                });
            }

            var reply = replyValue.GetString() ?? "";

            if (conversation == null)
            {
                conversation = new AIConversation
                {
                    UserId = userId,
                    Title = request.Message.Trim().Length > 100
                        ? request.Message.Trim()[..100]
                        : request.Message.Trim()
                };

                _db.AIConversations.Add(conversation);
            }

            conversation.Messages.Add(new AIMessage
            {
                SenderType = "USER",
                Content = request.Message.Trim()
            });

            conversation.Messages.Add(new AIMessage
            {
                SenderType = "ASSISTANT",
                Content = reply
            });

            await _db.SaveChangesAsync(cancellationToken);

            return Ok(new
            {
                success = true,
                reply,
                conversation_id = conversation.ConversationId
            });
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, new
            {
                success = false,
                message = "Không thể kết nối đến Flask AI."
            });
        }
        catch (TaskCanceledException)
            when (!cancellationToken.IsCancellationRequested)
        {
            return StatusCode(504, new
            {
                success = false,
                message = "Flask AI phản hồi quá thời gian."
            });
        }
    }

    [HttpGet("conversations/{conversationId:long}/messages")]
    public async Task<IActionResult> GetMessages(
        long conversationId,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        if (userId <= 0)
            return Unauthorized();

        var exists = await _db.AIConversations.AnyAsync(
            x => x.ConversationId == conversationId
              && x.UserId == userId,
            cancellationToken);

        if (!exists)
        {
            return NotFound(new
            {
                success = false,
                message = "Không tìm thấy cuộc hội thoại."
            });
        }

        var messages = await _db.AIMessages
            .Where(x => x.ConversationId == conversationId)
            .OrderBy(x => x.AIMessageId)
            .Select(x => new
            {
                x.AIMessageId,
                x.SenderType,
                x.Content
            })
            .ToListAsync(cancellationToken);

        return Ok(new
        {
            success = true,
            conversation_id = conversationId,
            messages
        });
    }
}

public class AiChatRequest
{
    [Required]
    public string Message { get; set; } = string.Empty;

    public long? ConversationId { get; set; }
}
