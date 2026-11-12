using System.Text.Json.Serialization;

namespace CollabSphere.API.Contracts;

public class ApiErrorResponse
{
    public bool Success { get; set; }
    public ApiErrorDetail Error { get; set; } = new();

    public static ApiErrorResponse From(
        int statusCode,
        string message,
        Dictionary<string, string[]>? details = null)
    {
        return new ApiErrorResponse
        {
            Success = false,
            Error = new ApiErrorDetail
            {
                Code = CodeFor(statusCode),
                Message = message,
                Details = details
            }
        };
    }

    public static string CodeFor(int statusCode)
    {
        return statusCode switch
        {
            400 => "VALIDATION_ERROR",
            401 => "UNAUTHORIZED",
            403 => "FORBIDDEN",
            404 => "NOT_FOUND",
            409 => "CONFLICT",
            503 => "SERVICE_UNAVAILABLE",
            >= 500 => "INTERNAL_ERROR",
            _ => "ERROR"
        };
    }
}

public class ApiErrorDetail
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, string[]>? Details { get; set; }
}
