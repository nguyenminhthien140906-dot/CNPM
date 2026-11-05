using System.Net;
using System.Text.Json;

namespace CollabSphere.API.Middleware;

public class Middleware
{
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
            _logger.LogError(
                ex,
                "Đã xảy ra lỗi khi xử lý request."
            );

            context.Response.Clear();
            context.Response.StatusCode =
                (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var response = new
            {
                Success = false,
                Message = "Đã xảy ra lỗi từ máy chủ.",
                StatusCode = 500
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }
    }
}

