
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
    public ICollection<Project> CreatedProjects { get; set; } = new List<Project>();
    public ICollection<Project> ApprovedProjects { get; set; } = new List<Project>();
    public ICollection<Resource> UploadedResources { get; set; } = new List<Resource>();
    public ICollection<Meeting> CreatedMeetings { get; set; } = new List<Meeting>();
    public ICollection<MeetingParticipant> MeetingParticipations { get; set; } = new List<MeetingParticipant>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public ICollection<WhiteboardSnapshot> WhiteboardSnapshots { get; set; } = new List<WhiteboardSnapshot>();
    public ICollection<WorkspaceDocument> CreatedDocuments { get; set; } = new List<WorkspaceDocument>();
    public ICollection<DocumentVersion> EditedDocumentVersions { get; set; } = new List<DocumentVersion>();
    public ICollection<Evaluation> GivenEvaluations { get; set; } = new List<Evaluation>();
    public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public ICollection<AIConversation> AIConversations { get; set; } = new List<AIConversation>();
}

public class Student
{
    public long StudentId { get; set; }
    public long UserId { get; set; }
    public string StudentCode { get; set; } = string.Empty;

    public User? User { get; set; }
    public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();
    public ICollection<Team> LedTeams { get; set; } = new List<Team>();
    public ICollection<TeamMember> TeamMemberships { get; set; } = new List<TeamMember>();
    public ICollection<TaskAssignee> AssignedTasks { get; set; } = new List<TaskAssignee>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
}

public class Lecturer
{
    public long LecturerId { get; set; }
    public long UserId { get; set; }
    public string LecturerCode { get; set; } = string.Empty;

    public User? User { get; set; }
    public ICollection<ClassLecturer> ClassLecturers { get; set; } = new List<ClassLecturer>();
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
    public ICollection<Project> Projects { get; set; } = new List<Project>();
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
    public ICollection<Project> Projects { get; set; } = new List<Project>();
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
    public Lecturer? Lecturer { get; set; }
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

    public StudyClass? StudyClass { get; set; }
    public Syllabus? Syllabus { get; set; }
    public User? Creator { get; set; }
    public User? Approver { get; set; }
    public ICollection<Team> Teams { get; set; } = new List<Team>();
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();
    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    public ICollection<Resource> Resources { get; set; } = new List<Resource>();
    public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
}

public class Team
{
    public long TeamId { get; set; }
    public long ProjectId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public long? LeaderStudentId { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }

    public Project? Project { get; set; }
    public Student? LeaderStudent { get; set; }
    public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    public ICollection<Resource> Resources { get; set; } = new List<Resource>();
    public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
    public Workspace? Workspace { get; set; }
    public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
}

public class TeamMember
{
    public long TeamId { get; set; }
    public long StudentId { get; set; }
    public DateTime JoinedAt { get; set; }

    public Team? Team { get; set; }
    public Student? Student { get; set; }
}

public class Milestone
{
    public long MilestoneId { get; set; }
    public long ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int SortOrder { get; set; }
    public string Status { get; set; } = "PENDING";
    public DateTime CreatedAt { get; set; }

    public Project? Project { get; set; }
    public ICollection<Checkpoint> Checkpoints { get; set; } = new List<Checkpoint>();
    public ICollection<WorkTask> Tasks { get; set; } = new List<WorkTask>();
}

public class Checkpoint
{
    public long CheckpointId { get; set; }
    public long MilestoneId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public decimal MaxScore { get; set; } = 100;
    public string Status { get; set; } = "PENDING";
    public DateTime CreatedAt { get; set; }

    public Milestone? Milestone { get; set; }
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
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

    public Team? Team { get; set; }
    public Milestone? Milestone { get; set; }
    public ICollection<TaskAssignee> Assignees { get; set; } = new List<TaskAssignee>();
    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}

public class TaskAssignee
{
    public long TaskId { get; set; }
    public long StudentId { get; set; }
    public DateTime AssignedAt { get; set; }

    public WorkTask? Task { get; set; }
    public Student? Student { get; set; }
}

public class Submission
{
    public long SubmissionId { get; set; }
    public long? TaskId { get; set; }
    public long? CheckpointId { get; set; }
    public long TeamId { get; set; }
    public long SubmittedBy { get; set; }
    public string? Content { get; set; }
    public string? FileUrl { get; set; }
    public decimal? Score { get; set; }
    public string? Feedback { get; set; }
    public DateTime SubmittedAt { get; set; }

    public WorkTask? Task { get; set; }
    public Checkpoint? Checkpoint { get; set; }
    public Team? Team { get; set; }
    public Student? Submitter { get; set; }
    public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
}

public class Resource
{
    public long ResourceId { get; set; }
    public long? ProjectId { get; set; }
    public long? TeamId { get; set; }
    public long UploadedBy { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ResourceUrl { get; set; } = string.Empty;
    public string? ResourceType { get; set; }
    public DateTime CreatedAt { get; set; }

    public Project? Project { get; set; }
    public Team? Team { get; set; }
    public User? Uploader { get; set; }
}

public class Meeting
{
    public long MeetingId { get; set; }
    public long TeamId { get; set; }
    public long CreatedBy { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? MeetingUrl { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public Team? Team { get; set; }
    public User? Creator { get; set; }
    public ICollection<MeetingParticipant> Participants { get; set; } = new List<MeetingParticipant>();
}

public class MeetingParticipant
{
    public long MeetingId { get; set; }
    public long UserId { get; set; }
    public DateTime? JoinedAt { get; set; }
    public string AttendanceStatus { get; set; } = "INVITED";

    public Meeting? Meeting { get; set; }
    public User? User { get; set; }
}

public class Message
{
    public long MessageId { get; set; }
    public long TeamId { get; set; }
    public long SenderId { get; set; }
    public string Content { get; set; } = string.Empty;
    public string MessageType { get; set; } = "TEXT";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Team? Team { get; set; }
    public User? Sender { get; set; }
}

public class Workspace
{
    public long WorkspaceId { get; set; }
    public long TeamId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Team? Team { get; set; }
    public ICollection<WhiteboardSnapshot> WhiteboardSnapshots { get; set; } = new List<WhiteboardSnapshot>();
    public ICollection<WorkspaceDocument> Documents { get; set; } = new List<WorkspaceDocument>();
}

public class WhiteboardSnapshot
{
    public long SnapshotId { get; set; }
    public long WorkspaceId { get; set; }
    public long CreatedBy { get; set; }
    public string SnapshotData { get; set; } = "{}";
    public DateTime CreatedAt { get; set; }

    public Workspace? Workspace { get; set; }
    public User? Creator { get; set; }
}

public class WorkspaceDocument
{
    public long DocumentId { get; set; }
    public long WorkspaceId { get; set; }
    public long CreatedBy { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Workspace? Workspace { get; set; }
    public User? Creator { get; set; }
    public ICollection<DocumentVersion> Versions { get; set; } = new List<DocumentVersion>();
}

public class DocumentVersion
{
    public long VersionId { get; set; }
    public long DocumentId { get; set; }
    public long EditedBy { get; set; }
    public int VersionNumber { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public WorkspaceDocument? Document { get; set; }
    public User? Editor { get; set; }
}

public class Evaluation
{
    public long EvaluationId { get; set; }
    public long EvaluatorUserId { get; set; }
    public long? SubmissionId { get; set; }
    public long? ProjectId { get; set; }
    public long? TeamId { get; set; }
    public long? StudentId { get; set; }
    public string EvaluationType { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? Evaluator { get; set; }
    public Submission? Submission { get; set; }
    public Project? Project { get; set; }
    public Team? Team { get; set; }
    public Student? Student { get; set; }
}

public class Notification
{
    public long NotificationId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }

    public User? User { get; set; }
}

public class AIConversation
{
    public long ConversationId { get; set; }
    public long UserId { get; set; }
    public string? Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
    public ICollection<AIMessage> Messages { get; set; } = new List<AIMessage>();
}

public class AIMessage
{
    public long AIMessageId { get; set; }
    public long ConversationId { get; set; }
    public string SenderType { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public AIConversation? Conversation { get; set; }
}
