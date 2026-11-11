using CollabSphere.API.Services;
using CollabSphere.Application.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CollabSphere.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(
        IAuthService authService,
        JwtTokenService jwtTokenService)
    {
        _authService = authService;
        _jwtTokenService = jwtTokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request)
    {
        var user = await _authService.AuthenticateAsync(request);

        if (user == null)
        {
            return Unauthorized(new
            {
                Success = false,
                Message = "Username or password is incorrect."
            });
        }

        var response = _jwtTokenService.GenerateToken(user);

        return Ok(new
        {
            Success = true,
            Message = "Login successful.",
            Data = response
        });
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new
        {
            Success = true,
            Message = "Token is valid.",
            Data = new
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Username = User.FindFirstValue(ClaimTypes.Name),
                Email = User.FindFirstValue(ClaimTypes.Email),
                Role = User.FindFirstValue(ClaimTypes.Role)
            }
        });
    }
}
