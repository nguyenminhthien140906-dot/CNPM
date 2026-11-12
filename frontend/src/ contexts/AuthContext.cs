
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CollabSphere.API.Services;

public interface IAuthContext
{
    bool IsAuthenticated { get; }

    ulong? UserId { get; }

    string? Username { get; }

    string? Email { get; }

    string? RoleCode { get; }

    bool HasRole(string roleCode);
}

public class AuthContext : IAuthContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? CurrentUser =>
        _httpContextAccessor.HttpContext?.User;

    public bool IsAuthenticated =>
        CurrentUser?.Identity?.IsAuthenticated ?? false;

    public ulong? UserId
    {
        get
        {
            var value = CurrentUser?.FindFirstValue(
                ClaimTypes.NameIdentifier
            );

            return ulong.TryParse(value, out var id)
                ? id
                : null;
        }
    }

    public string? Username =>
        CurrentUser?.FindFirstValue(ClaimTypes.Name);

    public string? Email =>
        CurrentUser?.FindFirstValue(ClaimTypes.Email);

    public string? RoleCode =>
        CurrentUser?.FindFirstValue(ClaimTypes.Role);

    public bool HasRole(string roleCode)
    {
        if (!IsAuthenticated ||
            string.IsNullOrWhiteSpace(roleCode))
        {
            return false;
        }

        return string.Equals(
            RoleCode,
            roleCode,
            StringComparison.OrdinalIgnoreCase
        );
    }
}

