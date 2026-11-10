using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CollabSphere.Application.Auth;
using Microsoft.IdentityModel.Tokens;

namespace CollabSphere.API.Services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public LoginResponseDto GenerateToken(AuthUserDto user)
    {
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("Missing Jwt:Key.");

        var issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException("Missing Jwt:Issuer.");

        var audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException("Missing Jwt:Audience.");

        var expiresIn = _configuration.GetValue<int>(
            "Jwt:ExpiresInMinutes", 60);

        if (expiresIn <= 0)
            throw new InvalidOperationException("Invalid JWT expiration.");

        var keyBytes = Convert.FromBase64String(key);

        if (keyBytes.Length < 32)
            throw new InvalidOperationException("JWT key must be at least 32 bytes.");

        var signingKey = new SymmetricSecurityKey(keyBytes);

        var credentials = new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.Role, user.RoleCode),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(expiresIn);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: credentials);

        return new LoginResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            TokenType = "Bearer",
            ExpiresIn = expiresIn * 60,
            User = user
        };
    }
}
