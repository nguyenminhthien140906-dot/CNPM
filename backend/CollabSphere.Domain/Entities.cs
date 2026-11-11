feature/leader-backend-core
﻿using System;


using System;
 main
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
public class Subject
{
    public long SubjectId { get; set; }
    public string SubjectCode { get; set; } = string.Empty;
    public string SubjectName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Syllabus> Syllabi { get; set; } = new List<Syllabus>();
    public ICollection<StudyClass> Classes { get; set; } = new List<StudyClass>();
}

public class Syllabus
{
    public long SyllabusId { get; set; }
    public long SubjectId { get; set; }
    public string Version { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public Subject? Subject { get; set; }
}

public class StudyClass
{
    public long ClassId { get; set; }
    public long SubjectId { get; set; }
    public string ClassCode { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string? Semester { get; set; }
    public string? AcademicYear { get; set; }
    public int? Capacity { get; set; }
    public string Status { get; set; } = "OPEN";
    public DateTime CreatedAt { get; set; }

    public Subject? Subject { get; set; }
    public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
    public ICollection<ClassLecturer> ClassLecturers { get; set; } = new List<ClassLecturer>();
}

public class Student
{
feature/leader-backend-core
    public class User
    {
        public string UserId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }

    public class Group
    {
        public string GroupId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public string LeaderId { get; set; } = string.Empty;
        public List<string> MemberIds { get; private set; } = new();
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
        public string TaskId { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public DomainTaskStatus Status { get; private set; } = DomainTaskStatus.ToDo;
        public string? AssigneeId { get; private set; }
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
        public string WorkspaceId { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string? WhiteboardData { get; private set; }
        public string? DocumentData { get; private set; }
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
        public string EvalId { get; set; } = string.Empty;
        public string EvaluatorId { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public EvaluationType EvalType { get; set; }
        public float Score { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public long StudentId { get; set; }
    public long UserId { get; set; }
    public string StudentCode { get; set; } = string.Empty;

    public User? User { get; set; }
}

public class Lecturer
{
    public long LecturerId { get; set; }
    public long UserId { get; set; }
    public string LecturerCode { get; set; } = string.Empty;

    public User? User { get; set; }
}

public class ClassStudent
{
    public long ClassId { get; set; }
    public long StudentId { get; set; }
    public DateTime EnrolledAt { get; set; }

    public StudyClass? StudyClass { get; set; }
    public Student? Student { get; set; }
}

public class ClassLecturer
{
    public long ClassId { get; set; }
    public long LecturerId { get; set; }
    public string AssignmentRole { get; set; } = "LECTURER";
    public DateTime AssignedAt { get; set; }

    public StudyClass? StudyClass { get; set; }
main
}
