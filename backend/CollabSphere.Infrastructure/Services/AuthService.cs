using CollabSphere.Domain.Entities;
using CollabSphere.Application.Auth;
using CollabSphere.Infrastructure.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<AuthUserDto?> AuthenticateAsync(LoginRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.UsernameOrEmail) ||
            string.IsNullOrWhiteSpace(request.Password))
            return null;

        var login = request.UsernameOrEmail.Trim();

        var user = await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u =>
                u.Username == login || u.Email == login
            );

        if (user == null || !user.IsActive || user.Role == null)
            return null;

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password
        );

        if (verificationResult == PasswordVerificationResult.Failed)
            return null;

        return new AuthUserDto
        {
            UserId = (ulong)user.UserId,
            Username = user.Username,
            Email = user.Email,
            FullName = user.FullName,
            RoleCode = user.Role.RoleCode
        };
    }
}
