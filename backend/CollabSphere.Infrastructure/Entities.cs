
using System;
using System.Collections.Generic;

namespace CollabSphere.Infrastructure.Entities;

public class EntityMetadata
{
    public string TableName { get; set; } = string.Empty;

    public string SchemaName { get; set; } = string.Empty;

    public string PrimaryKey { get; set; } = string.Empty;

    public bool IsAuditable { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

public class EntityAudit
{
    public long AuditId { get; set; }

    public string EntityName { get; set; } = string.Empty;

    public string EntityId { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? ChangedBy { get; set; }

    public DateTime ChangedAt { get; set; }
}

public class EntityValidationResult
{
    public bool IsValid { get; set; }

    public List<string> Errors { get; set; } = new();

    public void AddError(string error)
    {
        Errors.Add(error);
        IsValid = false;
    }
}
