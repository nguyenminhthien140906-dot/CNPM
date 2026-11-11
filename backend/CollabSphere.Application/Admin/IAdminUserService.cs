namespace CollabSphere.Application.Admin;

public interface IAdminUserService
{
    Task<AdminUserPageDto> GetUsersAsync(
        int page,
        int pageSize,
        string? search);

    Task<AdminUserDetailDto?> GetUserByIdAsync(ulong userId);

    Task<bool> SetUserStatusAsync(
        ulong userId,
        bool isActive);
}
