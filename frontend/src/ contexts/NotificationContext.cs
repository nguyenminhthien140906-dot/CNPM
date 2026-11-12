
using Microsoft.AspNetCore.Components;

namespace CollabSphere.Frontend.Contexts;

public enum NotificationType
{
    Success,
    Error,
    Warning,
    Info
}

public class NotificationItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Message { get; set; } = string.Empty;

    public NotificationType Type { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string Icon => Type switch
    {
        NotificationType.Success => "✓",
        NotificationType.Error => "✕",
        NotificationType.Warning => "⚠",
        NotificationType.Info => "ℹ",
        _ => "ℹ"
    };

    public string BackgroundColor => Type switch
    {
        NotificationType.Success => "#F0FDF4",
        NotificationType.Error => "#FEF2F2",
        NotificationType.Warning => "#FFFBEB",
        NotificationType.Info => "#EFF6FF",
        _ => "#EFF6FF"
    };

    public string BorderColor => Type switch
    {
        NotificationType.Success => "#86EFAC",
        NotificationType.Error => "#FCA5A5",
        NotificationType.Warning => "#FCD34D",
        NotificationType.Info => "#93C5FD",
        _ => "#93C5FD"
    };

    public string IconColor => Type switch
    {
        NotificationType.Success => "#16A34A",
        NotificationType.Error => "#DC2626",
        NotificationType.Warning => "#D97706",
        NotificationType.Info => "#2563EB",
        _ => "#2563EB"
    };
}

public class NotificationContext : IDisposable
{
    private readonly List<NotificationItem> _notifications = new();

    private readonly List<CancellationTokenSource> _timers = new();

    public IReadOnlyList<NotificationItem> Notifications =>
        _notifications.AsReadOnly();

    public event Action? OnChange;

    public Guid AddNotification(
        string message,
        NotificationType type = NotificationType.Info,
        int duration = 4000)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException(
                "Message cannot be empty.",
                nameof(message)
            );
        }

        var notification = new NotificationItem
        {
            Id = Guid.NewGuid(),
            Message = message,
            Type = type,
            CreatedAt = DateTime.UtcNow
        };

        _notifications.Add(notification);

        OnChange?.Invoke();

        if (duration > 0)
        {
            var cancellationToken = new CancellationTokenSource();

            _timers.Add(cancellationToken);

            _ = AutoRemoveAsync(
                notification.Id,
                duration,
                cancellationToken
            );
        }

        return notification.Id;
    }

    public Guid Success(string message, int duration = 4000)
    {
        return AddNotification(
            message,
            NotificationType.Success,
            duration
        );
    }

    public Guid Error(string message, int duration = 5000)
    {
        return AddNotification(
            message,
            NotificationType.Error,
            duration
        );
    }

    public Guid Warning(string message, int duration = 4000)
    {
        return AddNotification(
            message,
            NotificationType.Warning,
            duration
        );
    }

    public Guid Info(string message, int duration = 4000)
    {
        return AddNotification(
            message,
            NotificationType.Info,
            duration
        );
    }

    public bool RemoveNotification(Guid id)
    {
        var notification = _notifications.FirstOrDefault(
            item => item.Id == id
        );

        if (notification == null)
        {
            return false;
        }

        var removed = _notifications.Remove(notification);

        if (removed)
        {
            OnChange?.Invoke();
        }

        return removed;
    }

    public void ClearNotifications()
    {
        _notifications.Clear();

        foreach (var timer in _timers)
        {
            timer.Cancel();
            timer.Dispose();
        }

        _timers.Clear();

        OnChange?.Invoke();
    }

    private async Task AutoRemoveAsync(
        Guid id,
        int duration,
        CancellationTokenSource cancellationToken)
    {
        try
        {
            await Task.Delay(
                duration,
                cancellationToken.Token
            );

            RemoveNotification(id);
        }
        catch (OperationCanceledException)
        {
        }
        finally
        {
            _timers.Remove(cancellationToken);
            cancellationToken.Dispose();
        }
    }

    public void Dispose()
    {
        foreach (var timer in _timers)
        {
            timer.Cancel();
            timer.Dispose();
        }

        _timers.Clear();
        _notifications.Clear();
    }
}
