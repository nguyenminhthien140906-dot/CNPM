
using CollabSphere.API.Hubs;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.SignalR;

namespace CollabSphere.API.Services;

public class NotificationService
{
    private readonly ApplicationDbContext _db;
    private readonly IHubContext<NotificationHub> _hub;

    public NotificationService(
        ApplicationDbContext db,
        IHubContext<NotificationHub> hub)
    {
        _db = db;
        _hub = hub;
    }

    public async Task<Notification> SendAsync(
        long userId,
        string title,
        string message)
    {
        if (string.IsNullOrWhiteSpace(title) ||
            string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException(
                "Nội dung thông báo không được để trống.");
        }

        var notification = new Notification
        {
            UserId = userId,
            Title = title.Trim(),
            Message = message.Trim(),
            IsRead = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.Notifications.Add(notification);
        await _db.SaveChangesAsync();

        await _hub.Clients
            .Group($"user-{userId}")
            .SendAsync("ReceiveNotification", new
            {
                notificationId = notification.NotificationId,
                title = notification.Title,
                message = notification.Message,
                isRead = notification.IsRead,
                createdAt = notification.CreatedAt
            });

        return notification;
    }
}
