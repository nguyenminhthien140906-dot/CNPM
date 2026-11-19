
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CollabSphere.API.Hubs;

[Authorize]
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!long.TryParse(userId, out var id))
        {
            Context.Abort();
            return;
        }

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"user-{id}"
        );

        await base.OnConnectedAsync();
    }
}
