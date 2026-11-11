namespace CollabSphere.Application.Admin;

public class AdminUserListItemDto
{
    public ulong UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string RoleCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class AdminUserDetailDto : AdminUserListItemDto
{
    public string? AvatarUrl { get; set; }
    public string? Phone { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class AdminUserPageDto
{
    public IEnumerable<AdminUserListItemDto> Items { get; set; }
        = Enumerable.Empty<AdminUserListItemDto>();

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}

public class UpdateUserStatusRequestDto
{
    public bool IsActive { get; set; }
}
