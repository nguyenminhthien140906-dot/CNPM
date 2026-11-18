
using System.Security.Claims;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Hubs;

[Authorize]
public class ChatHub : Hub
{
    private readonly ApplicationDbContext _db;

    public ChatHub(ApplicationDbContext db)
    {
        _db = db;
    }

    private long GetUserId()
    {
        var value = Context.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!long.TryParse(value, out var userId))
            throw new HubException("Tài khoản không hợp lệ.");

        return userId;
    }

    private async Task<bool> IsTeamMemberAsync(long teamId, long userId)
    {
        return await _db.Set<TeamMember>()
            .AnyAsync(tm =>
                tm.TeamId == teamId &&
                _db.Set<Student>().Any(s =>
                    s.StudentId == tm.StudentId &&
                    s.UserId == userId));
    }

    public async Task JoinTeam(long teamId)
    {
        var userId = GetUserId();

        if (!await IsTeamMemberAsync(teamId, userId))
            throw new HubException("Bạn không thuộc nhóm này.");

        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"team-{teamId}");
    }

    public async Task LeaveTeam(long teamId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            $"team-{teamId}");
    }

    public async Task SendMessage(long teamId, string content)
    {
        var userId = GetUserId();

        if (!await IsTeamMemberAsync(teamId, userId))
            throw new HubException("Bạn không có quyền gửi tin nhắn.");

        if (string.IsNullOrWhiteSpace(content) ||
            content.Length > 5000)
            throw new HubException("Nội dung tin nhắn không hợp lệ.");

        var message = new Message
        {
            TeamId = teamId,
            SenderId = userId,
            Content = content.Trim(),
            MessageType = "TEXT",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _db.Set<Message>().Add(message);
        await _db.SaveChangesAsync();

        await Clients.Group($"team-{teamId}")
            .SendAsync("ReceiveMessage", new
            {
                messageId = message.MessageId,
                teamId = message.TeamId,
                senderId = message.SenderId,
                content = message.Content,
                createdAt = message.CreatedAt
            });
    }
}
