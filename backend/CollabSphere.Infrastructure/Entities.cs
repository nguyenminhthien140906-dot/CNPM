namespace CollabSphere.Infrastructure.Entities;

public class Role
{
    public ulong RoleId { get; set; }

    public string RoleCode { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}

public class User
{
    public ulong UserId { get; set; }

    public ulong RoleId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string? AvatarUrl { get; set; }

    public string? Phone { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Role? Role { get; set; }
}

public class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int OwnerId { get; set; }

    public User? Owner { get; set; }

    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}

public class WorkTask
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public int ProjectId { get; set; }

    public Project? Project { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
