namespace CollabSphere.Application.Auth;

public class LoginRequestDto
{
    public string UsernameOrEmail { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;

    public string TokenType { get; set; } = "Bearer";

    public int ExpiresIn { get; set; }

    public AuthUserDto User { get; set; } = new();
}

public class AuthUserDto
{
    public ulong UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string RoleCode { get; set; } = string.Empty;
}
