using System.Security.Claims;
using CollabSphere.API.Authorization;
using CollabSphere.Application.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollabSphere.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = RolePolicies.AdminOnly)]
public class AdminController : ControllerBase
{
    private readonly IAdminUserService _adminUserService;

    public AdminController(IAdminUserService adminUserService)
    {
        _adminUserService = adminUserService;
    }

    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            Success = true,
            Message = "Admin authorization successful.",
            Data = new
            {
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            }
        });
    }

    // GET: api/admin/users?page=1&pageSize=20&search=abc
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null)
    {
        var result = await _adminUserService.GetUsersAsync(
            page,
            pageSize,
            search
        );

        return Ok(new
        {
            Success = true,
            Message = "Users retrieved successfully.",
            Data = result
        });
    }

    // GET: api/admin/users/{id}
    [HttpGet("users/{id:long}")]
    public async Task<IActionResult> GetUserById(ulong id)
    {
        var user = await _adminUserService.GetUserByIdAsync(id);

        if (user == null)
        {
            return NotFound(new
            {
                Success = false,
                Message = "User not found."
            });
        }

        return Ok(new
        {
            Success = true,
            Message = "User retrieved successfully.",
            Data = user
        });
    }

    // PATCH: api/admin/users/{id}/status
    [HttpPatch("users/{id:long}/status")]
    public async Task<IActionResult> UpdateUserStatus(
        ulong id,
        [FromBody] UpdateUserStatusRequestDto request)
    {
        var currentUserId = User.FindFirstValue(
            ClaimTypes.NameIdentifier
        );

        if (!ulong.TryParse(currentUserId, out var adminId))
        {
            return Unauthorized(new
            {
                Success = false,
                Message = "Invalid admin identity."
            });
        }

        if (adminId == id && !request.IsActive)
        {
            return BadRequest(new
            {
                Success = false,
                Message = "You cannot deactivate your own account."
            });
        }

        var updated = await _adminUserService.SetUserStatusAsync(
            id,
            request.IsActive
        );

        if (!updated)
        {
            return NotFound(new
            {
                Success = false,
                Message = "User not found."
            });
        }

        return Ok(new
        {
            Success = true,
            Message = request.IsActive
                ? "User account activated successfully."
                : "User account deactivated successfully."
        });
    }
}
