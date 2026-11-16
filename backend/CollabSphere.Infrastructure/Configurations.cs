
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollabSphere.Domain.Entities;

namespace CollabSphere.Infrastructure;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> b)
    {
        b.ToTable("roles");
        b.HasKey(x => x.RoleId);
        b.Property(x => x.RoleId).HasColumnName("role_id").ValueGeneratedOnAdd();
        b.Property(x => x.RoleCode).HasColumnName("role_code").HasMaxLength(30).IsRequired();
        b.Property(x => x.RoleName).HasColumnName("role_name").HasMaxLength(100).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.HasIndex(x => x.RoleCode).IsUnique();
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> b)
    {
        b.ToTable("users");
        b.HasKey(x => x.UserId);
        b.Property(x => x.UserId).HasColumnName("user_id").ValueGeneratedOnAdd();
        b.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
        b.Property(x => x.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
        b.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
        b.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
        b.Property(x => x.FullName).HasColumnName("full_name").HasMaxLength(150).IsRequired();
        b.Property(x => x.AvatarUrl).HasColumnName("avatar_url").HasMaxLength(500);
        b.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(20);
        b.Property(x => x.IsActive).HasColumnName("is_active").HasDefaultValue(true);
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => x.Username).IsUnique();
        b.HasIndex(x => x.Email).IsUnique();

        b.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> b)
    {
        b.ToTable("students");
        b.HasKey(x => x.StudentId);
        b.Property(x => x.StudentId).HasColumnName("student_id").ValueGeneratedOnAdd();
        b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        b.Property(x => x.StudentCode).HasColumnName("student_code").HasMaxLength(30).IsRequired();

        b.HasIndex(x => x.UserId).IsUnique();
        b.HasIndex(x => x.StudentCode).IsUnique();

        b.HasOne(x => x.User).WithOne()
            .HasForeignKey<Student>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
{
    public void Configure(EntityTypeBuilder<Lecturer> b)
    {
        b.ToTable("lecturers");
        b.HasKey(x => x.LecturerId);
        b.Property(x => x.LecturerId).HasColumnName("lecturer_id").ValueGeneratedOnAdd();
        b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        b.Property(x => x.LecturerCode).HasColumnName("lecturer_code").HasMaxLength(30).IsRequired();

        b.HasIndex(x => x.UserId).IsUnique();
        b.HasIndex(x => x.LecturerCode).IsUnique();

        b.HasOne(x => x.User).WithOne()
            .HasForeignKey<Lecturer>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> b)
    {
        b.ToTable("subjects");
        b.HasKey(x => x.SubjectId);
        b.Property(x => x.SubjectId).HasColumnName("subject_id").ValueGeneratedOnAdd();
        b.Property(x => x.SubjectCode).HasColumnName("subject_code").HasMaxLength(30).IsRequired();
        b.Property(x => x.SubjectName).HasColumnName("subject_name").HasMaxLength(150).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.HasIndex(x => x.SubjectCode).IsUnique();
    }
}

public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
{
    public void Configure(EntityTypeBuilder<Syllabus> b)
    {
        b.ToTable("syllabi");
        b.HasKey(x => x.SyllabusId);
        b.Property(x => x.SyllabusId).HasColumnName("syllabus_id").ValueGeneratedOnAdd();
        b.Property(x => x.SubjectId).HasColumnName("subject_id").IsRequired();
        b.Property(x => x.Version).HasColumnName("version").HasMaxLength(30).IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Content).HasColumnName("content").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.HasIndex(x => new { x.SubjectId, x.Version }).IsUnique();

        b.HasOne(x => x.Subject).WithMany(x => x.Syllabi)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class StudyClassConfiguration : IEntityTypeConfiguration<StudyClass>
{
    public void Configure(EntityTypeBuilder<StudyClass> b)
    {
        b.ToTable("study_classes", t =>
        {
            t.HasCheckConstraint("ck_study_classes_capacity", "capacity IS NULL OR capacity >= 0");
            t.HasCheckConstraint("ck_study_classes_status", "status IN ('PLANNED','OPEN','CLOSED','ARCHIVED')");
        });

        b.HasKey(x => x.ClassId);
        b.Property(x => x.ClassId).HasColumnName("class_id").ValueGeneratedOnAdd();
        b.Property(x => x.SubjectId).HasColumnName("subject_id").IsRequired();
        b.Property(x => x.ClassCode).HasColumnName("class_code").HasMaxLength(30).IsRequired();
        b.Property(x => x.ClassName).HasColumnName("class_name").HasMaxLength(150).IsRequired();
        b.Property(x => x.Semester).HasColumnName("semester").HasMaxLength(30);
        b.Property(x => x.AcademicYear).HasColumnName("academic_year").HasMaxLength(20);
        b.Property(x => x.Capacity).HasColumnName("capacity");
        b.Property(x => x.Status).HasColumnName("status").HasMaxLength(20).HasDefaultValue("OPEN");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.HasIndex(x => x.ClassCode).IsUnique();

        b.HasOne(x => x.Subject).WithMany(x => x.Classes)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class ClassStudentConfiguration : IEntityTypeConfiguration<ClassStudent>
{
    public void Configure(EntityTypeBuilder<ClassStudent> b)
    {
        b.ToTable("class_students");
        b.HasKey(x => new { x.ClassId, x.StudentId });
        b.Property(x => x.ClassId).HasColumnName("class_id");
        b.Property(x => x.StudentId).HasColumnName("student_id");
        b.Property(x => x.EnrolledAt).HasColumnName("enrolled_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasOne(x => x.StudyClass).WithMany(x => x.ClassStudents)
            .HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Student).WithMany(x => x.ClassStudents)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class ClassLecturerConfiguration : IEntityTypeConfiguration<ClassLecturer>
{
    public void Configure(EntityTypeBuilder<ClassLecturer> b)
    {
        b.ToTable("class_lecturers");
        b.HasKey(x => new { x.ClassId, x.LecturerId });
        b.Property(x => x.ClassId).HasColumnName("class_id");
        b.Property(x => x.LecturerId).HasColumnName("lecturer_id");
        b.Property(x => x.AssignmentRole).HasColumnName("assignment_role").HasMaxLength(50).HasDefaultValue("LECTURER");
        b.Property(x => x.AssignedAt).HasColumnName("assigned_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasOne(x => x.StudyClass).WithMany(x => x.ClassLecturers)
            .HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Lecturer).WithMany(x => x.ClassLecturers)
            .HasForeignKey(x => x.LecturerId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> b)
    {
        b.ToTable("projects", t =>
        {
            t.HasCheckConstraint("ck_projects_dates", "start_date IS NULL OR due_date IS NULL OR start_date <= due_date");
            t.HasCheckConstraint("ck_projects_group_size", "min_group_size >= 1 AND max_group_size >= min_group_size");
        });

        b.HasKey(x => x.ProjectId);
        b.Property(x => x.ProjectId).HasColumnName("project_id").ValueGeneratedOnAdd();
        b.Property(x => x.ClassId).HasColumnName("class_id").IsRequired();
        b.Property(x => x.SyllabusId).HasColumnName("syllabus_id");
        b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.Goals).HasColumnName("goals").HasColumnType("text");
        b.Property(x => x.ExpectedOutputs).HasColumnName("expected_outputs").HasColumnType("text");
        b.Property(x => x.Requirements).HasColumnName("requirements").HasColumnType("text");
        b.Property(x => x.StartDate).HasColumnName("start_date").HasColumnType("date");
        b.Property(x => x.DueDate).HasColumnName("due_date").HasColumnType("date");
        b.Property(x => x.MinGroupSize).HasColumnName("min_group_size").HasDefaultValue(1);
        b.Property(x => x.MaxGroupSize).HasColumnName("max_group_size").HasDefaultValue(5);
        b.Property(x => x.Status).HasColumnName("status").HasMaxLength(30).HasDefaultValue("DRAFT");
        b.Property(x => x.ApprovedBy).HasColumnName("approved_by");
        b.Property(x => x.ApprovedAt).HasColumnName("approved_at");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => x.ClassId);
        b.HasIndex(x => x.CreatedBy);

        b.HasOne(x => x.StudyClass).WithMany(x => x.Projects)
            .HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Syllabus).WithMany(x => x.Projects)
            .HasForeignKey(x => x.SyllabusId).OnDelete(DeleteBehavior.SetNull);

        b.HasOne(x => x.Creator).WithMany(x => x.CreatedProjects)
            .HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.Approver).WithMany(x => x.ApprovedProjects)
            .HasForeignKey(x => x.ApprovedBy).OnDelete(DeleteBehavior.SetNull);

        b.Ignore(x => x.Tasks);
    }
}

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> b)
    {
        b.ToTable("teams");
        b.HasKey(x => x.TeamId);
        b.Property(x => x.TeamId).HasColumnName("team_id").ValueGeneratedOnAdd();
        b.Property(x => x.ProjectId).HasColumnName("project_id").IsRequired();
        b.Property(x => x.TeamName).HasColumnName("team_name").HasMaxLength(100).IsRequired();
        b.Property(x => x.LeaderStudentId).HasColumnName("leader_student_id");
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => new { x.ProjectId, x.TeamName }).IsUnique();
        b.HasIndex(x => x.ProjectId);

        b.HasOne(x => x.Project).WithMany(x => x.Teams)
            .HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.LeaderStudent).WithMany(x => x.LedTeams)
            .HasForeignKey(x => x.LeaderStudentId).OnDelete(DeleteBehavior.SetNull);
    }
}

public class TeamMemberConfiguration : IEntityTypeConfiguration<TeamMember>
{
    public void Configure(EntityTypeBuilder<TeamMember> b)
    {
        b.ToTable("team_members");
        b.HasKey(x => new { x.TeamId, x.StudentId });
        b.Property(x => x.TeamId).HasColumnName("team_id");
        b.Property(x => x.StudentId).HasColumnName("student_id");
        b.Property(x => x.JoinedAt).HasColumnName("joined_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasOne(x => x.Team).WithMany(x => x.Members)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Student).WithMany(x => x.TeamMemberships)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class MilestoneConfiguration : IEntityTypeConfiguration<Milestone>
{
    public void Configure(EntityTypeBuilder<Milestone> b)
    {
        b.ToTable("milestones", t =>
            t.HasCheckConstraint("ck_milestones_sort_order", "sort_order >= 0"));

        b.HasKey(x => x.MilestoneId);
        b.Property(x => x.MilestoneId).HasColumnName("milestone_id").ValueGeneratedOnAdd();
        b.Property(x => x.ProjectId).HasColumnName("project_id").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.DueDate).HasColumnName("due_date").HasColumnType("date");
        b.Property(x => x.SortOrder).HasColumnName("sort_order").HasDefaultValue(0);
        b.Property(x => x.Status).HasColumnName("status").HasMaxLength(30).HasDefaultValue("PENDING");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => x.ProjectId);

        b.HasOne(x => x.Project).WithMany(x => x.Milestones)
            .HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class CheckpointConfiguration : IEntityTypeConfiguration<Checkpoint>
{
    public void Configure(EntityTypeBuilder<Checkpoint> b)
    {
        b.ToTable("checkpoints", t =>
            t.HasCheckConstraint("ck_checkpoints_max_score", "max_score >= 0"));

        b.HasKey(x => x.CheckpointId);
        b.Property(x => x.CheckpointId).HasColumnName("checkpoint_id").ValueGeneratedOnAdd();
        b.Property(x => x.MilestoneId).HasColumnName("milestone_id").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.DueDate).HasColumnName("due_date");
        b.Property(x => x.MaxScore).HasColumnName("max_score").HasPrecision(5, 2).HasDefaultValue(100m);
        b.Property(x => x.Status).HasColumnName("status").HasMaxLength(30).HasDefaultValue("PENDING");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => x.MilestoneId);

        b.HasOne(x => x.Milestone).WithMany(x => x.Checkpoints)
            .HasForeignKey(x => x.MilestoneId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> b)
    {
        b.ToTable("tasks");
        b.HasKey(x => x.TaskId);
        b.Property(x => x.TaskId).HasColumnName("task_id").ValueGeneratedOnAdd();
        b.Property(x => x.TeamId).HasColumnName("team_id").IsRequired();
        b.Property(x => x.MilestoneId).HasColumnName("milestone_id");
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.Deadline).HasColumnName("deadline");
        b.Property(x => x.Status).HasColumnName("status").HasMaxLength(30).HasDefaultValue("TODO");
        b.Property(x => x.Priority).HasColumnName("priority").HasMaxLength(30).HasDefaultValue("MEDIUM");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => x.TeamId);
        b.HasIndex(x => x.MilestoneId);

        b.HasOne(x => x.Team).WithMany(x => x.Tasks)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Milestone).WithMany(x => x.Tasks)
            .HasForeignKey(x => x.MilestoneId).OnDelete(DeleteBehavior.SetNull);
    }
}

public class TaskAssigneeConfiguration : IEntityTypeConfiguration<TaskAssignee>
{
    public void Configure(EntityTypeBuilder<TaskAssignee> b)
    {
        b.ToTable("task_assignees");
        b.HasKey(x => new { x.TaskId, x.StudentId });
        b.Property(x => x.TaskId).HasColumnName("task_id");
        b.Property(x => x.StudentId).HasColumnName("student_id");
        b.Property(x => x.AssignedAt).HasColumnName("assigned_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasOne(x => x.Task).WithMany(x => x.Assignees)
            .HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Student).WithMany(x => x.AssignedTasks)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
{
    public void Configure(EntityTypeBuilder<Submission> b)
    {
        b.ToTable("submissions", t =>
        {
            t.HasCheckConstraint("ck_submissions_single_target",
                "(task_id IS NOT NULL AND checkpoint_id IS NULL) OR (task_id IS NULL AND checkpoint_id IS NOT NULL)");
            t.HasCheckConstraint("ck_submissions_score", "score IS NULL OR score >= 0");
        });

        b.HasKey(x => x.SubmissionId);
        b.Property(x => x.SubmissionId).HasColumnName("submission_id").ValueGeneratedOnAdd();
        b.Property(x => x.TaskId).HasColumnName("task_id");
        b.Property(x => x.CheckpointId).HasColumnName("checkpoint_id");
        b.Property(x => x.TeamId).HasColumnName("team_id").IsRequired();
        b.Property(x => x.SubmittedBy).HasColumnName("submitted_by").IsRequired();
        b.Property(x => x.Content).HasColumnName("content").HasColumnType("text");
        b.Property(x => x.FileUrl).HasColumnName("file_url").HasMaxLength(1000);
        b.Property(x => x.Score).HasColumnName("score").HasPrecision(5, 2);
        b.Property(x => x.Feedback).HasColumnName("feedback").HasColumnType("text");
        b.Property(x => x.SubmittedAt).HasColumnName("submitted_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => x.TeamId);
        b.HasIndex(x => x.SubmittedBy);

        b.HasOne(x => x.Task).WithMany(x => x.Submissions)
            .HasForeignKey(x => x.TaskId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Checkpoint).WithMany(x => x.Submissions)
            .HasForeignKey(x => x.CheckpointId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Team).WithMany(x => x.Submissions)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Submitter).WithMany(x => x.Submissions)
            .HasForeignKey(x => x.SubmittedBy).OnDelete(DeleteBehavior.Restrict);
    }
}

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> b)
    {
        b.ToTable("resources", t =>
            t.HasCheckConstraint("ck_resources_single_owner",
                "(project_id IS NOT NULL AND team_id IS NULL) OR (project_id IS NULL AND team_id IS NOT NULL)"));

        b.HasKey(x => x.ResourceId);
        b.Property(x => x.ResourceId).HasColumnName("resource_id").ValueGeneratedOnAdd();
        b.Property(x => x.ProjectId).HasColumnName("project_id");
        b.Property(x => x.TeamId).HasColumnName("team_id");
        b.Property(x => x.UploadedBy).HasColumnName("uploaded_by").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.ResourceUrl).HasColumnName("resource_url").HasMaxLength(1000).IsRequired();
        b.Property(x => x.ResourceType).HasColumnName("resource_type").HasMaxLength(50);
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => x.ProjectId);
        b.HasIndex(x => x.TeamId);

        b.HasOne(x => x.Project).WithMany(x => x.Resources)
            .HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Team).WithMany(x => x.Resources)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Uploader).WithMany(x => x.UploadedResources)
            .HasForeignKey(x => x.UploadedBy).OnDelete(DeleteBehavior.Restrict);
    }
}

public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> b)
    {
        b.ToTable("meetings", t =>
            t.HasCheckConstraint("ck_meetings_time", "ends_at IS NULL OR ends_at > starts_at"));

        b.HasKey(x => x.MeetingId);
        b.Property(x => x.MeetingId).HasColumnName("meeting_id").ValueGeneratedOnAdd();
        b.Property(x => x.TeamId).HasColumnName("team_id").IsRequired();
        b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Description).HasColumnName("description").HasColumnType("text");
        b.Property(x => x.MeetingUrl).HasColumnName("meeting_url").HasMaxLength(500);
        b.Property(x => x.StartsAt).HasColumnName("starts_at").IsRequired();
        b.Property(x => x.EndsAt).HasColumnName("ends_at");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => x.TeamId);
        b.HasIndex(x => x.StartsAt);

        b.HasOne(x => x.Team).WithMany(x => x.Meetings)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Creator).WithMany(x => x.CreatedMeetings)
            .HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}

public class MeetingParticipantConfiguration : IEntityTypeConfiguration<MeetingParticipant>
{
    public void Configure(EntityTypeBuilder<MeetingParticipant> b)
    {
        b.ToTable("meeting_participants");
        b.HasKey(x => new { x.MeetingId, x.UserId });
        b.Property(x => x.MeetingId).HasColumnName("meeting_id");
        b.Property(x => x.UserId).HasColumnName("user_id");
        b.Property(x => x.JoinedAt).HasColumnName("joined_at");
        b.Property(x => x.AttendanceStatus).HasColumnName("attendance_status").HasMaxLength(30).HasDefaultValue("INVITED");

        b.HasOne(x => x.Meeting).WithMany(x => x.Participants)
            .HasForeignKey(x => x.MeetingId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.User).WithMany(x => x.MeetingParticipations)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> b)
    {
        b.ToTable("messages");
        b.HasKey(x => x.MessageId);
        b.Property(x => x.MessageId).HasColumnName("message_id").ValueGeneratedOnAdd();
        b.Property(x => x.TeamId).HasColumnName("team_id").IsRequired();
        b.Property(x => x.SenderId).HasColumnName("sender_id").IsRequired();
        b.Property(x => x.Content).HasColumnName("content").HasColumnType("text").IsRequired();
        b.Property(x => x.MessageType).HasColumnName("message_type").HasMaxLength(30).HasDefaultValue("TEXT");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => new { x.TeamId, x.CreatedAt });

        b.HasOne(x => x.Team).WithMany(x => x.Messages)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Sender).WithMany(x => x.Messages)
            .HasForeignKey(x => x.SenderId).OnDelete(DeleteBehavior.Restrict);
    }
}

public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> b)
    {
        b.ToTable("workspaces");
        b.HasKey(x => x.WorkspaceId);
        b.Property(x => x.WorkspaceId).HasColumnName("workspace_id").ValueGeneratedOnAdd();
        b.Property(x => x.TeamId).HasColumnName("team_id").IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => x.TeamId).IsUnique();

        b.HasOne(x => x.Team).WithOne(x => x.Workspace)
            .HasForeignKey<Workspace>(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class WhiteboardSnapshotConfiguration : IEntityTypeConfiguration<WhiteboardSnapshot>
{
    public void Configure(EntityTypeBuilder<WhiteboardSnapshot> b)
    {
        b.ToTable("whiteboard_snapshots");
        b.HasKey(x => x.SnapshotId);
        b.Property(x => x.SnapshotId).HasColumnName("snapshot_id").ValueGeneratedOnAdd();
        b.Property(x => x.WorkspaceId).HasColumnName("workspace_id").IsRequired();
        b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        b.Property(x => x.SnapshotData).HasColumnName("snapshot_data").HasColumnType("json").IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => new { x.WorkspaceId, x.CreatedAt });

        b.HasOne(x => x.Workspace).WithMany(x => x.WhiteboardSnapshots)
            .HasForeignKey(x => x.WorkspaceId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Creator).WithMany(x => x.WhiteboardSnapshots)
            .HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}

public class WorkspaceDocumentConfiguration : IEntityTypeConfiguration<WorkspaceDocument>
{
    public void Configure(EntityTypeBuilder<WorkspaceDocument> b)
    {
        b.ToTable("workspace_documents");
        b.HasKey(x => x.DocumentId);
        b.Property(x => x.DocumentId).HasColumnName("document_id").ValueGeneratedOnAdd();
        b.Property(x => x.WorkspaceId).HasColumnName("workspace_id").IsRequired();
        b.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Content).HasColumnName("content").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => x.WorkspaceId);

        b.HasOne(x => x.Workspace).WithMany(x => x.Documents)
            .HasForeignKey(x => x.WorkspaceId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Creator).WithMany(x => x.CreatedDocuments)
            .HasForeignKey(x => x.CreatedBy).OnDelete(DeleteBehavior.Restrict);
    }
}

public class DocumentVersionConfiguration : IEntityTypeConfiguration<DocumentVersion>
{
    public void Configure(EntityTypeBuilder<DocumentVersion> b)
    {
        b.ToTable("document_versions", t =>
            t.HasCheckConstraint("ck_document_versions_number", "version_number > 0"));

        b.HasKey(x => x.VersionId);
        b.Property(x => x.VersionId).HasColumnName("version_id").ValueGeneratedOnAdd();
        b.Property(x => x.DocumentId).HasColumnName("document_id").IsRequired();
        b.Property(x => x.EditedBy).HasColumnName("edited_by").IsRequired();
        b.Property(x => x.VersionNumber).HasColumnName("version_number").IsRequired();
        b.Property(x => x.Content).HasColumnName("content").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => new { x.DocumentId, x.VersionNumber }).IsUnique();
        b.HasIndex(x => x.DocumentId);

        b.HasOne(x => x.Document).WithMany(x => x.Versions)
            .HasForeignKey(x => x.DocumentId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Editor).WithMany(x => x.EditedDocumentVersions)
            .HasForeignKey(x => x.EditedBy).OnDelete(DeleteBehavior.Restrict);
    }
}

public class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
{
    public void Configure(EntityTypeBuilder<Evaluation> b)
    {
        b.ToTable("evaluations", t =>
        {
            t.HasCheckConstraint("ck_evaluations_score", "score >= 0");
            t.HasCheckConstraint("ck_evaluations_single_target",
                "(submission_id IS NOT NULL) + (project_id IS NOT NULL) + (team_id IS NOT NULL) + (student_id IS NOT NULL) = 1");
        });

        b.HasKey(x => x.EvaluationId);
        b.Property(x => x.EvaluationId).HasColumnName("evaluation_id").ValueGeneratedOnAdd();
        b.Property(x => x.EvaluatorUserId).HasColumnName("evaluator_user_id").IsRequired();
        b.Property(x => x.SubmissionId).HasColumnName("submission_id");
        b.Property(x => x.ProjectId).HasColumnName("project_id");
        b.Property(x => x.TeamId).HasColumnName("team_id");
        b.Property(x => x.StudentId).HasColumnName("student_id");
        b.Property(x => x.EvaluationType).HasColumnName("evaluation_type").HasMaxLength(30).IsRequired();
        b.Property(x => x.Score).HasColumnName("score").HasPrecision(5, 2).IsRequired();
        b.Property(x => x.Comment).HasColumnName("comment").HasColumnType("text");
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => x.EvaluatorUserId);
        b.HasIndex(x => x.SubmissionId);
        b.HasIndex(x => x.ProjectId);
        b.HasIndex(x => x.TeamId);
        b.HasIndex(x => x.StudentId);

        b.HasOne(x => x.Evaluator).WithMany(x => x.GivenEvaluations)
            .HasForeignKey(x => x.EvaluatorUserId).OnDelete(DeleteBehavior.Restrict);

        b.HasOne(x => x.Submission).WithMany(x => x.Evaluations)
            .HasForeignKey(x => x.SubmissionId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Project).WithMany(x => x.Evaluations)
            .HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Team).WithMany(x => x.Evaluations)
            .HasForeignKey(x => x.TeamId).OnDelete(DeleteBehavior.Cascade);

        b.HasOne(x => x.Student).WithMany(x => x.Evaluations)
            .HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> b)
    {
        b.ToTable("notifications");
        b.HasKey(x => x.NotificationId);
        b.Property(x => x.NotificationId).HasColumnName("notification_id").ValueGeneratedOnAdd();
        b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        b.Property(x => x.Message).HasColumnName("message").HasColumnType("text").IsRequired();
        b.Property(x => x.IsRead).HasColumnName("is_read").HasDefaultValue(false);
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => new { x.UserId, x.IsRead, x.CreatedAt });

        b.HasOne(x => x.User).WithMany(x => x.Notifications)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class AIConversationConfiguration : IEntityTypeConfiguration<AIConversation>
{
    public void Configure(EntityTypeBuilder<AIConversation> b)
    {
        b.ToTable("ai_conversations");
        b.HasKey(x => x.ConversationId);
        b.Property(x => x.ConversationId).HasColumnName("conversation_id").ValueGeneratedOnAdd();
        b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        b.Property(x => x.Title).HasColumnName("title").HasMaxLength(200);
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
        b.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        b.HasIndex(x => new { x.UserId, x.CreatedAt });

        b.HasOne(x => x.User).WithMany(x => x.AIConversations)
            .HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class AIMessageConfiguration : IEntityTypeConfiguration<AIMessage>
{
    public void Configure(EntityTypeBuilder<AIMessage> b)
    {
        b.ToTable("ai_messages");
        b.HasKey(x => x.AIMessageId);
        b.Property(x => x.AIMessageId).HasColumnName("ai_message_id").ValueGeneratedOnAdd();
        b.Property(x => x.ConversationId).HasColumnName("conversation_id").IsRequired();
        b.Property(x => x.SenderType).HasColumnName("sender_type").HasMaxLength(20).IsRequired();
        b.Property(x => x.Content).HasColumnName("content").HasColumnType("text").IsRequired();
        b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");

        b.HasIndex(x => new { x.ConversationId, x.CreatedAt });

        b.HasOne(x => x.Conversation).WithMany(x => x.Messages)
            .HasForeignKey(x => x.ConversationId).OnDelete(DeleteBehavior.Cascade);
    }
}
