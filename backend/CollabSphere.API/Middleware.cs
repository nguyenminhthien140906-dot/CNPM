using System.Text.Json;
using CollabSphere.API.Contracts;

namespace CollabSphere.API.Middleware;

public class Middleware
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    private readonly RequestDelegate _next;
    private readonly ILogger<Middleware> _logger;

    public Middleware(
        RequestDelegate next,
        ILogger<Middleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Đã xảy ra lỗi khi xử lý request.");

            context.Response.Clear();
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = ApiErrorResponse.From(500, "Đã xảy ra lỗi từ máy chủ.");

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, JsonOptions));
        }
    }
}
