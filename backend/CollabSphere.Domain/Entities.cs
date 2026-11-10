using System;
using System.Collections.Generic;
using CollabSphere.Domain.Enums;
using DomainTaskStatus = CollabSphere.Domain.Enums.TaskStatus;

namespace CollabSphere.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }

    public class Group
    {
        public string GroupId { get; set; }
        public string Name { get; set; }
        public string CourseId { get; set; }
        public string LeaderId { get; set; }
        public List<string> MemberIds { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Group(string groupId, string name, string courseId, string leaderId)
        {
            GroupId = groupId;
            Name = name;
            CourseId = courseId;
            LeaderId = leaderId;
            MemberIds = new List<string> { leaderId };
        }

        public void AddMember(string studentId)
        {
            if (!MemberIds.Contains(studentId))
                MemberIds.Add(studentId);
        }

        public void RemoveMember(string studentId)
        {
            if (MemberIds.Contains(studentId) && studentId != LeaderId)
                MemberIds.Remove(studentId);
        }
    }

    public class Task
    {
        public string TaskId { get; set; }
        public string GroupId { get; set; }
        public string Title { get; set; }
        public DateTime Deadline { get; set; }
        public DomainTaskStatus Status { get; private set; } = DomainTaskStatus.ToDo;
        public string AssigneeId { get; private set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public void AssignTo(string studentId)
        {
            AssigneeId = studentId;
            Status = DomainTaskStatus.InProgress;
        }

        public void UpdateStatus(DomainTaskStatus newStatus)
        {
            Status = newStatus;
        }
    }

    public class Workspace
    {
        public string WorkspaceId { get; set; }
        public string GroupId { get; set; }
        public string WhiteboardData { get; private set; }
        public string DocumentData { get; private set; }
        public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;

        public void SaveSnapshot(string whiteboardPayload, string documentPayload)
        {
            WhiteboardData = whiteboardPayload;
            DocumentData = documentPayload;
            LastUpdated = DateTime.UtcNow;
        }
    }

    public class Evaluation
    {
        public string EvalId { get; set; }
        public string EvaluatorId { get; set; }
        public string TargetId { get; set; }
        public EvaluationType EvalType { get; set; }
        public float Score { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
