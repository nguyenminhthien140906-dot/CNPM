
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[Route("api/meetings")]
[Authorize]
public class MeetingsController : ApiControllerBase
{
    private readonly ApplicationDbContext _db;

    public MeetingsController(ApplicationDbContext db)
    {
        _db = db;
    }

    private long GetUserId()
    {
        var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return long.TryParse(value, out var id) ? id : 0;
    }

    private async Task<bool> IsTeamMember(long teamId, long userId)
    {
        return await _db.Set<TeamMember>().AnyAsync(tm =>
            tm.TeamId == teamId &&
            _db.Set<Student>().Any(s =>
                s.StudentId == tm.StudentId &&
                s.UserId == userId));
    }

    [HttpGet("team/{teamId:long}")]
    public async Task<IActionResult> GetByTeam(long teamId)
    {
        if (!await IsTeamMember(teamId, GetUserId()))
            return Failure(403, "Ban khong thuoc nhom nay.");

        var meetings = await _db.Set<Meeting>()
            .AsNoTracking()
            .Where(m => m.TeamId == teamId)
            .OrderByDescending(m => m.StartsAt)
            .Select(m => new
            {
                m.MeetingId,
                m.TeamId,
                m.Title,
                m.Description,
                m.MeetingUrl,
                m.StartsAt,
                m.EndsAt,
                m.CreatedBy
            })
            .ToListAsync();

        return Success(meetings);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateMeetingRequest request)
    {
        var userId = GetUserId();

        if (!await IsTeamMember(request.TeamId, userId))
            return Failure(403, "Ban khong thuoc nhom nay.");

        if (request.EndsAt.HasValue &&
            request.EndsAt.Value <= request.StartsAt)
            return Failure(400, "Thoi gian ket thuc phai sau bat dau.");

        var meeting = new Meeting
        {
            TeamId = request.TeamId,
            CreatedBy = userId,
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            MeetingUrl = request.MeetingUrl,
            StartsAt = request.StartsAt.ToUniversalTime(),
            EndsAt = request.EndsAt?.ToUniversalTime(),
            CreatedAt = DateTime.UtcNow
        };

        _db.Set<Meeting>().Add(meeting);
        await _db.SaveChangesAsync();

        return CreatedOk(new
        {
            meeting.MeetingId,
            meeting.TeamId,
            meeting.Title,
            meeting.MeetingUrl,
            meeting.StartsAt,
            meeting.EndsAt
        });
    }

    [HttpPost("{meetingId:long}/join")]
    public async Task<IActionResult> Join(long meetingId)
    {
        var userId = GetUserId();

        var meeting = await _db.Set<Meeting>()
            .FirstOrDefaultAsync(m => m.MeetingId == meetingId);

        if (meeting == null)
            return Failure(404, "Khong tim thay cuoc hop.");

        if (!await IsTeamMember(meeting.TeamId, userId))
            return Failure(403, "Ban khong thuoc nhom nay.");

        var participant = await _db.Set<MeetingParticipant>()
            .FirstOrDefaultAsync(p =>
                p.MeetingId == meetingId && p.UserId == userId);

        if (participant == null)
        {
            participant = new MeetingParticipant
            {
                MeetingId = meetingId,
                UserId = userId,
                JoinedAt = DateTime.UtcNow,
                AttendanceStatus = "JOINED"
            };

            _db.Set<MeetingParticipant>().Add(participant);
        }
        else
        {
            participant.JoinedAt ??= DateTime.UtcNow;
            participant.AttendanceStatus = "JOINED";
        }

        await _db.SaveChangesAsync();

        return Success(new
        {
            meeting.MeetingId,
            meeting.MeetingUrl,
            participant.AttendanceStatus
        });
    }

    [HttpPut("{meetingId:long}")]
    public async Task<IActionResult> Update(
        long meetingId,
        [FromBody] UpdateMeetingRequest request)
    {
        var userId = GetUserId();

        var meeting = await _db.Set<Meeting>()
            .FirstOrDefaultAsync(m => m.MeetingId == meetingId);

        if (meeting == null)
            return Failure(404, "Khong tim thay cuoc hop.");

        if (!await IsTeamMember(meeting.TeamId, userId))
            return Failure(403, "Ban khong thuoc nhom nay.");

        if (meeting.CreatedBy != userId)
            return Failure(403, "Chi nguoi tao moi duoc cap nhat.");

        if (request.EndsAt.HasValue &&
            request.EndsAt.Value <= request.StartsAt)
            return Failure(400, "Thoi gian ket thuc khong hop le.");

        meeting.Title = request.Title.Trim();
        meeting.Description = request.Description?.Trim();
        meeting.MeetingUrl = request.MeetingUrl;
        meeting.StartsAt = request.StartsAt.ToUniversalTime();
        meeting.EndsAt = request.EndsAt?.ToUniversalTime();

        await _db.SaveChangesAsync();

        return Success(new
        {
            meeting.MeetingId,
            meeting.Title,
            meeting.MeetingUrl,
            meeting.StartsAt,
            meeting.EndsAt
        });
    }

    [HttpDelete("{meetingId:long}")]
    public async Task<IActionResult> Delete(long meetingId)
    {
        var userId = GetUserId();

        var meeting = await _db.Set<Meeting>()
            .FirstOrDefaultAsync(m => m.MeetingId == meetingId);

        if (meeting == null)
            return Failure(404, "Khong tim thay cuoc hop.");

        if (!await IsTeamMember(meeting.TeamId, userId))
            return Failure(403, "Ban khong thuoc nhom nay.");

        if (meeting.CreatedBy != userId)
            return Failure(403, "Chi nguoi tao moi duoc huy.");

        _db.Set<Meeting>().Remove(meeting);
        await _db.SaveChangesAsync();

        return Success(new { meetingId }, "Da huy cuoc hop.");
    }

}


public class CreateMeetingRequest
{
    [Range(1, long.MaxValue)]
    public long TeamId { get; set; }

    [Required, StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [StringLength(500)]
    [Url]
    public string? MeetingUrl { get; set; }

    [Required]
    public DateTime StartsAt { get; set; }

    public DateTime? EndsAt { get; set; }
}

public class UpdateMeetingRequest
{
    [Required, StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [StringLength(500)]
    [Url]
    public string? MeetingUrl { get; set; }

    [Required]
    public DateTime StartsAt { get; set; }

    public DateTime? EndsAt { get; set; }
}
