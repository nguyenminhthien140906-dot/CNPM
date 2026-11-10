namespace CollabSphere.Application.Auth;

public interface IAuthService
{
    Task<AuthUserDto?> AuthenticateAsync(LoginRequestDto request);
}
