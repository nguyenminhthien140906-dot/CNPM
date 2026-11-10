using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CollabSphere.Infrastructure.Persistence;

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
            return StatusCode(503, result);

        return Ok(result);
    }

    [HttpGet("database-check")]
    public async Task<IActionResult> DatabaseCheck()
    {
        try
        {
            var roleCount = await _context.Roles.CountAsync();
            var userCount = await _context.Users.CountAsync();

            await _context.Roles.AsNoTracking().Take(1).ToListAsync();
            await _context.Users.AsNoTracking().Take(1).ToListAsync();

            return Ok(new
            {
                Success = true,
                Message = "Identity tables and column mappings are accessible.",
                Tables = new
                {
                    Roles = roleCount,
                    Users = userCount
                },
                ColumnMapping = "Passed"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                Success = false,
                Message = "Identity mapping check failed.",
                Error = ex.Message
            });
        }
    }
}
