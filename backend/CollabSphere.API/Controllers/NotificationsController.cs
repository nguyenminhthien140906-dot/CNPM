
using System.Security.Claims;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[ApiController]
[Authorize]
[Route("api/notifications")]
public class NotificationsController : ControllerBase
{
   private readonly ApplicationDbContext _db;

public NotificationsController(ApplicationDbContext db)
{
    _db = db;
}

    private long? GetUserId()
    {
        var value = User.FindFirst(
            ClaimTypes.NameIdentifier)?.Value;

        return long.TryParse(value, out var id)
            ? id
            : null;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized();

        var notifications = await _db.Notifications
            .AsNoTracking()
            .Where(n => n.UserId == userId.Value)
            .OrderByDescending(n => n.CreatedAt)
            .Take(100)
            .ToListAsync();

        return Ok(notifications);
    }

    [HttpPatch("{id:long}/read")]
    public async Task<IActionResult> MarkAsRead(long id)
    {
        var userId = GetUserId();

        if (userId == null)
            return Unauthorized();

        var notification = await _db.Notifications
            .FirstOrDefaultAsync(n =>
                n.NotificationId == id &&
                n.UserId == userId.Value);

        if (notification == null)
            return NotFound();

        notification.IsRead = true;
        await _db.SaveChangesAsync();

        return Ok(new
        {
            message = "Đã đọc thông báo"
        });
    }
}
