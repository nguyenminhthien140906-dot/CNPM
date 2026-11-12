
using System;

namespace CollabSphere.Client.Hooks;

public class AuthStateService
{
    private UserSession? _currentUser;

    public UserSession? CurrentUser => _currentUser;

    public bool IsAuthenticated =>
        _currentUser != null && _currentUser.IsAuthenticated;

    public string? UserId => _currentUser?.UserId;

    public string? FullName => _currentUser?.FullName;

    public string? Email => _currentUser?.Email;

    public string? Role => _currentUser?.Role;

    public event Action? OnAuthStateChanged;

    public void SetUser(UserSession user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _currentUser = user;
        _currentUser.IsAuthenticated = true;

        NotifyStateChanged();
    }

    public void UpdateUser(UserSession user)
    {
        if (!IsAuthenticated)
            return;

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _currentUser = user;
        _currentUser.IsAuthenticated = true;

        NotifyStateChanged();
    }

    public void ClearUser()
    {
        _currentUser = null;

        NotifyStateChanged();
    }

    public bool HasRole(string role)
    {
        if (!IsAuthenticated || string.IsNullOrWhiteSpace(role))
            return false;

        return string.Equals(
            _currentUser?.Role,
            role,
            StringComparison.OrdinalIgnoreCase
        );
    }

    public bool HasAnyRole(params string[] roles)
    {
        if (!IsAuthenticated || roles == null)
            return false;

        return roles.Any(HasRole);
    }

    public void NotifyStateChanged()
    {
        OnAuthStateChanged?.Invoke();
    }
}

public class UserSession
{
    public string UserId { get; set; } = "";

    public string FullName { get; set; } = "";

    public string Email { get; set; } = "";

    public string Role { get; set; } = "";

    public string? Avatar { get; set; }

    public bool IsAuthenticated { get; set; }
}

