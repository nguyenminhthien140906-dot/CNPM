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

        var query = _context.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();

            query = query.Where(u =>
                u.Username.Contains(keyword) ||
                u.Email.Contains(keyword) ||
                u.FullName.Contains(keyword));
        }

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderByDescending(u => u.UserId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(u => new AdminUserListItemDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                FullName = u.FullName,
                RoleCode = u.Role != null
                    ? u.Role.RoleCode
                    : string.Empty,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt
            })
            .ToListAsync();

        return new AdminUserPageDto
        {
            Items = users,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(
                totalCount / (double)pageSize)
        };
    }

    public async Task<AdminUserDetailDto?> GetUserByIdAsync(
        ulong userId)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.UserId == userId)
            .Select(u => new AdminUserDetailDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email,
                FullName = u.FullName,
                RoleCode = u.Role != null
                    ? u.Role.RoleCode
                    : string.Empty,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt,
                AvatarUrl = u.AvatarUrl,
                Phone = u.Phone
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SetUserStatusAsync(
        ulong userId,
        bool isActive)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserId == userId);

        if (user == null)
        {
            return false;
        }

        user.IsActive = isActive;

        await _context.SaveChangesAsync();

        return true;
    }
}
