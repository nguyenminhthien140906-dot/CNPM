using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CollabSphere.API.Authorization;

namespace CollabSphere.API.Controllers;

[ApiController]
[Route("api/admin")]
[Authorize(Policy = RolePolicies.AdminOnly)]
public class AdminController : ControllerBase
{
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok(new
        {
            Success = true,
            Message = "Admin authorization successful.",
            Data = new
            {
                Role = User.FindFirst(
                    System.Security.Claims.ClaimTypes.Role
                )?.Value
            }
        });
    }
}
