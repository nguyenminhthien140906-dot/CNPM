using Microsoft.AspNetCore.Mvc;
using CollabSphere.API.Data;

namespace CollabSphere.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SystemController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SystemController(ApplicationDbContext context)
    {
        _context = context;
    }


    [HttpGet("status")]
    public async Task<IActionResult> GetStatus()
    {
        bool databaseConnected;

        try
        {
            databaseConnected = await _context.Database.CanConnectAsync();
        }
        catch
        {
            databaseConnected = false;
        }

        var result = new
        {
            Application = "CollabSphere API",
            Status = "Running",
            Database = databaseConnected ? "Connected" : "Disconnected",
            ServerTime = DateTime.Now
        };

        if (!databaseConnected)
        {
            return StatusCode(503, result);
        }

        return Ok(result);
    }
}

