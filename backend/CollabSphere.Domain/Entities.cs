
using System;
using System.Collections.Generic;

namespace CollabSphere.Domain.Entities;

public class Role
{
    public long RoleId { get; set; }
    public string RoleCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}

public class User
{
    public long UserId { get; set; }
    public long RoleId { get; set; }
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

    public ICollection<Project> CreatedProjects { get; set; }
        = new List<Project>();
}

public class Project
{
    public long ProjectId { get; set; }
    public long ClassId { get; set; }
    public long? SyllabusId { get; set; }
    public long CreatedBy { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Goals { get; set; }
    public string? ExpectedOutputs { get; set; }
    public string? Requirements { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; }

    public int MinGroupSize { get; set; } = 1;
    public int MaxGroupSize { get; set; } = 5;

    public string Status { get; set; } = "DRAFT";
    public long? ApprovedBy { get; set; }
    public DateTime? ApprovedAt { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? Creator { get; set; }
    public User? Approver { get; set; }

    public ICollection<WorkTask> Tasks { get; set; }
        = new List<WorkTask>();
}

public class WorkTask
{
    public long TaskId { get; set; }
    public long TeamId { get; set; }
    public long? MilestoneId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }

    public DateTime? Deadline { get; set; }

    public string Status { get; set; } = "TODO";
    public string Priority { get; set; } = "MEDIUM";

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
