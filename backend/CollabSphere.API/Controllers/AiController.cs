
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollabSphere.API.Controllers;

[ApiController]
[Route("api/ai")]
[Authorize]
public class AiController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AiController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
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

        try
        {
            var client = _httpClientFactory.CreateClient("FlaskAI");

            using var response = await client.PostAsJsonAsync(
                "/api/chat",
                new { message = request.Message },
                cancellationToken);

            var result = await response.Content.ReadAsStringAsync(
                cancellationToken);

            return new ContentResult
            {
                StatusCode = (int)response.StatusCode,
                ContentType = "application/json",
                Content = result
            };
        }
        catch (HttpRequestException)
        {
            return StatusCode(503, new
            {
                success = false,
                message = "Không thể kết nối đến Flask AI."
            });
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return StatusCode(504, new
            {
                success = false,
                message = "Flask AI phản hồi quá thời gian."
            });
        }
    }
}

public class AiChatRequest
{
    [Required]
    public string Message { get; set; } = string.Empty;
}
