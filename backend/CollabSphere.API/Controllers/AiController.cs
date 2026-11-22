
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

    // =====================================================
    // 1. AI CHATBOT
    // =====================================================

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

    // =====================================================
    // 2. LỊCH SỬ HỘI THOẠI AI
    // =====================================================

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

    // =====================================================
    // 3. PHÂN TÍCH TIẾN ĐỘ NHÓM
    // =====================================================

    [HttpGet("teams/{teamId:long}/progress")]
    public async Task<IActionResult> GetTeamProgress(
        long teamId,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        if (userId <= 0)
            return Unauthorized();

        var team = await _db.Teams
            .Where(t => t.TeamId == teamId)
            .Select(t => new
            {
                t.TeamId,
                t.TeamName,
                t.ProjectId,
                t.Project!.ClassId,
                t.Project.CreatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (team == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Không tìm thấy nhóm."
            });
        }

        var isMember = await _db.TeamMembers.AnyAsync(
            tm => tm.TeamId == teamId &&
                  _db.Students.Any(s =>
                      s.StudentId == tm.StudentId &&
                      s.UserId == userId),
            cancellationToken);

        var isLecturer = await _db.ClassLecturers.AnyAsync(
            cl => cl.ClassId == team.ClassId &&
                  _db.Lecturers.Any(l =>
                      l.LecturerId == cl.LecturerId &&
                      l.UserId == userId),
            cancellationToken);

        if (!isMember && !isLecturer && team.CreatedBy != userId)
        {
            return StatusCode(403, new
            {
                success = false,
                message = "Bạn không có quyền xem tiến độ nhóm này."
            });
        }

        var tasks = await _db.Tasks
    .Where(t => t.TeamId == teamId)
    .Select(t => new
    {
        t.Status,
        t.Deadline
    })
    .ToListAsync(cancellationToken);

        var total = tasks.Count;

        var completed = tasks.Count(t =>
            string.Equals(
                t.Status,
                "DONE",
                StringComparison.OrdinalIgnoreCase));

        var overdue = tasks.Count(t =>
            !string.Equals(
                t.Status,
                "DONE",
                StringComparison.OrdinalIgnoreCase)
            && t.Deadline.HasValue
            && t.Deadline.Value < DateTime.UtcNow);

        var completionRate = total == 0
            ? 0
            : Math.Round(completed * 100.0 / total, 2);

        return Ok(new
        {
            success = true,
            data = new
            {
                team_id = team.TeamId,
                team_name = team.TeamName,
                total_tasks = total,
                completed_tasks = completed,
                overdue_tasks = overdue,
                completion_rate = completionRate
            }
        });
    }

    // =====================================================
    // 4. PHÂN TÍCH ĐÓNG GÓP THÀNH VIÊN
    // =====================================================

    [HttpGet("teams/{teamId:long}/contributions")]
    public async Task<IActionResult> GetTeamContributions(
        long teamId,
        CancellationToken cancellationToken)
    {
        var userId = GetUserId();

        if (userId <= 0)
            return Unauthorized();

        var team = await _db.Teams
            .Where(t => t.TeamId == teamId)
            .Select(t => new
            {
                t.TeamId,
                t.TeamName,
                ClassId = t.Project!.ClassId,
                CreatedBy = t.Project.CreatedBy
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (team == null)
        {
            return NotFound(new
            {
                success = false,
                message = "Không tìm thấy nhóm."
            });
        }

        var isMember = await _db.TeamMembers.AnyAsync(
            tm => tm.TeamId == teamId &&
                  _db.Students.Any(s =>
                      s.StudentId == tm.StudentId &&
                      s.UserId == userId),
            cancellationToken);

        var isLecturer = await _db.ClassLecturers.AnyAsync(
            cl => cl.ClassId == team.ClassId &&
                  _db.Lecturers.Any(l =>
                      l.LecturerId == cl.LecturerId &&
                      l.UserId == userId),
            cancellationToken);

        if (!isMember && !isLecturer && team.CreatedBy != userId)
        {
            return StatusCode(403, new
            {
                success = false,
                message = "Bạn không có quyền xem đóng góp nhóm này."
            });
        }

        var members = await _db.TeamMembers
            .Where(tm => tm.TeamId == teamId)
            .Select(tm => new
            {
                tm.StudentId,
                FullName = tm.Student!.User!.FullName
            })
            .ToListAsync(cancellationToken);

        var tasks = await _db.Tasks
    .Where(t => t.TeamId == teamId)
    .Select(t => new
    {
        t.Status,
        StudentIds = t.Assignees
            .Select(a => a.StudentId)
            .ToList()
    })
    .ToListAsync(cancellationToken);

        var contributions = members.Select(m =>
        {
            var assigned = tasks
                .Where(t => t.StudentIds.Contains(m.StudentId))
                .ToList();

            var completed = assigned.Count(t =>
                string.Equals(
                    t.Status,
                    "DONE",
                    StringComparison.OrdinalIgnoreCase));

            return new
            {
                student_id = m.StudentId,
                full_name = m.FullName,
                assigned_tasks = assigned.Count,
                completed_tasks = completed,
                completion_rate = assigned.Count == 0
                    ? 0
                    : Math.Round(
                        completed * 100.0 / assigned.Count,
                        2)
            };
        }).ToList();

        return Ok(new
        {
            success = true,
            team_id = team.TeamId,
            team_name = team.TeamName,
            contributions
        });
    }
}

// =====================================================
// REQUEST MODEL
// =====================================================

public class AiChatRequest
{
    [Required]
    public string Message { get; set; } = string.Empty;

    public long? ConversationId { get; set; }
}
