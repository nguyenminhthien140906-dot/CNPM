using CollabSphere.Application.Admin;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.Infrastructure.Services;

public class AdminUserService : IAdminUserService
{
    private readonly ApplicationDbContext _context;

    public AdminUserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<AdminUserPageDto> GetUsersAsync(
        int page,
        int pageSize,
        string? search)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 100);

        var query = _context.Users.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();

            query = query.Where(u =>
                u.Username.Contains(keyword) ||
                u.Email.Contains(keyword) ||
                u.FullName.Contains(keyword));
        }

        var totalCount = await query.CountAsync();

        var rows = await query
            .OrderByDescending(u => u.UserId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new
            {
                u.UserId,
                u.Username,
                u.Email,
                u.FullName,
                RoleCode = u.Role != null ? u.Role.RoleCode : string.Empty,
                u.IsActive,
                u.CreatedAt
            })
            .ToListAsync();

        var users = rows
            .Select(r => new AdminUserListItemDto
            {
                UserId = (ulong)r.UserId,
                Username = r.Username,
                Email = r.Email,
                FullName = r.FullName,
                RoleCode = r.RoleCode,
                IsActive = r.IsActive,
                CreatedAt = r.CreatedAt
            })
            .ToList();

        return new AdminUserPageDto
        {
            Items = users,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<AdminUserDetailDto?> GetUserByIdAsync(ulong userId)
    {
        var id = (long)userId;

        var row = await _context.Users
            .AsNoTracking()
            .Where(u => u.UserId == id)
            .Select(u => new
            {
                u.UserId,
                u.Username,
                u.Email,
                u.FullName,
                RoleCode = u.Role != null ? u.Role.RoleCode : string.Empty,
                u.IsActive,
                u.CreatedAt,
                u.UpdatedAt,
                u.AvatarUrl,
                u.Phone
            })
            .FirstOrDefaultAsync();

        if (row == null)
        {
            return null;
        }

        return new AdminUserDetailDto
        {
            UserId = (ulong)row.UserId,
            Username = row.Username,
            Email = row.Email,
            FullName = row.FullName,
            RoleCode = row.RoleCode,
            IsActive = row.IsActive,
            CreatedAt = row.CreatedAt,
            UpdatedAt = row.UpdatedAt,
            AvatarUrl = row.AvatarUrl,
            Phone = row.Phone
        };
    }

    public async Task<bool> SetUserStatusAsync(ulong userId, bool isActive)
    {
        var id = (long)userId;

        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null)
        {
            return false;
        }

        user.IsActive = isActive;

        await _context.SaveChangesAsync();

        return true;
    }
}
