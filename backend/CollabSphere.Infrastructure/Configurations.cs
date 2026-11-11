using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CollabSphere.Domain.Entities;

namespace CollabSphere.Infrastructure;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        builder.HasKey(x => x.RoleId);

        builder.Property(x => x.RoleId).HasColumnName("role_id").ValueGeneratedOnAdd();
        builder.Property(x => x.RoleCode).HasColumnName("role_code").HasMaxLength(30).IsRequired();
        builder.Property(x => x.RoleName).HasColumnName("role_name").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

        builder.HasIndex(x => x.RoleCode).IsUnique();
    }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.UserId).HasColumnName("user_id").ValueGeneratedOnAdd();
        builder.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
        builder.Property(x => x.Username).HasColumnName("username").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
        builder.Property(x => x.PasswordHash).HasColumnName("password_hash").HasMaxLength(255).IsRequired();
        builder.Property(x => x.FullName).HasColumnName("full_name").HasMaxLength(150).IsRequired();
        builder.Property(x => x.AvatarUrl).HasColumnName("avatar_url").HasMaxLength(500);
        builder.Property(x => x.Phone).HasColumnName("phone").HasMaxLength(20);
        builder.Property(x => x.IsActive).HasColumnName("is_active").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.Email).IsUnique();

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("projects");
        builder.HasKey(x => x.ProjectId);

        builder.Property(x => x.ProjectId).HasColumnName("project_id").ValueGeneratedOnAdd();
        builder.Property(x => x.ClassId).HasColumnName("class_id").IsRequired();
        builder.Property(x => x.SyllabusId).HasColumnName("syllabus_id");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Goals).HasColumnName("goals");
        builder.Property(x => x.ExpectedOutputs).HasColumnName("expected_outputs");
        builder.Property(x => x.Requirements).HasColumnName("requirements");
        builder.Property(x => x.StartDate).HasColumnName("start_date").HasColumnType("date");
        builder.Property(x => x.DueDate).HasColumnName("due_date").HasColumnType("date");
        builder.Property(x => x.MinGroupSize).HasColumnName("min_group_size").IsRequired();
        builder.Property(x => x.MaxGroupSize).HasColumnName("max_group_size").IsRequired();
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(30).IsRequired();
        builder.Property(x => x.ApprovedBy).HasColumnName("approved_by");
        builder.Property(x => x.ApprovedAt).HasColumnName("approved_at");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

        builder.Ignore(x => x.Tasks);

        builder.HasOne(x => x.Creator)
            .WithMany(x => x.CreatedProjects)
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Approver)
            .WithMany()
            .HasForeignKey(x => x.ApprovedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

public class WorkTaskConfiguration : IEntityTypeConfiguration<WorkTask>
{
    public void Configure(EntityTypeBuilder<WorkTask> builder)
    {
        builder.ToTable("tasks");
        builder.HasKey(x => x.TaskId);

        builder.Property(x => x.TaskId).HasColumnName("task_id").ValueGeneratedOnAdd();
        builder.Property(x => x.TeamId).HasColumnName("team_id").IsRequired();
        builder.Property(x => x.MilestoneId).HasColumnName("milestone_id");
        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Deadline).HasColumnName("deadline");
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Priority).HasColumnName("priority").HasMaxLength(30).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();
    }
}

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.ToTable("subjects");
        builder.HasKey(x => x.SubjectId);

        builder.Property(x => x.SubjectId).HasColumnName("subject_id").ValueGeneratedOnAdd();
        builder.Property(x => x.SubjectCode).HasColumnName("subject_code").HasMaxLength(30).IsRequired();
        builder.Property(x => x.SubjectName).HasColumnName("subject_name").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

        builder.HasIndex(x => x.SubjectCode).IsUnique();
    }
}

public class SyllabusConfiguration : IEntityTypeConfiguration<Syllabus>
{
    public void Configure(EntityTypeBuilder<Syllabus> builder)
    {
        builder.ToTable("syllabi");
        builder.HasKey(x => x.SyllabusId);

        builder.Property(x => x.SyllabusId).HasColumnName("syllabus_id").ValueGeneratedOnAdd();
        builder.Property(x => x.SubjectId).HasColumnName("subject_id").IsRequired();
        builder.Property(x => x.Version).HasColumnName("version").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Title).HasColumnName("title").HasMaxLength(200).IsRequired();
        builder.Property(x => x.Content).HasColumnName("content");
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

        builder.HasIndex(x => new { x.SubjectId, x.Version }).IsUnique();

        builder.HasOne(x => x.Subject)
            .WithMany(x => x.Syllabi)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class StudyClassConfiguration : IEntityTypeConfiguration<StudyClass>
{
    public void Configure(EntityTypeBuilder<StudyClass> builder)
    {
        builder.ToTable("study_classes");
        builder.HasKey(x => x.ClassId);

        builder.Property(x => x.ClassId).HasColumnName("class_id").ValueGeneratedOnAdd();
        builder.Property(x => x.SubjectId).HasColumnName("subject_id").IsRequired();
        builder.Property(x => x.ClassCode).HasColumnName("class_code").HasMaxLength(30).IsRequired();
        builder.Property(x => x.ClassName).HasColumnName("class_name").HasMaxLength(150).IsRequired();
        builder.Property(x => x.Semester).HasColumnName("semester").HasMaxLength(30);
        builder.Property(x => x.AcademicYear).HasColumnName("academic_year").HasMaxLength(20);
        builder.Property(x => x.Capacity).HasColumnName("capacity");
        builder.Property(x => x.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

        builder.HasIndex(x => x.ClassCode).IsUnique();

        builder.HasOne(x => x.Subject)
            .WithMany(x => x.Classes)
            .HasForeignKey(x => x.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("students");
        builder.HasKey(x => x.StudentId);

        builder.Property(x => x.StudentId).HasColumnName("student_id").ValueGeneratedOnAdd();
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(x => x.StudentCode).HasColumnName("student_code").HasMaxLength(30).IsRequired();

        builder.HasIndex(x => x.UserId).IsUnique();
        builder.HasIndex(x => x.StudentCode).IsUnique();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
{
    public void Configure(EntityTypeBuilder<Lecturer> builder)
    {
        builder.ToTable("lecturers");
        builder.HasKey(x => x.LecturerId);

        builder.Property(x => x.LecturerId).HasColumnName("lecturer_id").ValueGeneratedOnAdd();
        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(x => x.LecturerCode).HasColumnName("lecturer_code").HasMaxLength(30).IsRequired();

        builder.HasIndex(x => x.UserId).IsUnique();
        builder.HasIndex(x => x.LecturerCode).IsUnique();

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ClassStudentConfiguration : IEntityTypeConfiguration<ClassStudent>
{
    public void Configure(EntityTypeBuilder<ClassStudent> builder)
    {
        builder.ToTable("class_students");
        builder.HasKey(x => new { x.ClassId, x.StudentId });

        builder.Property(x => x.ClassId).HasColumnName("class_id");
        builder.Property(x => x.StudentId).HasColumnName("student_id");
        builder.Property(x => x.EnrolledAt).HasColumnName("enrolled_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

        builder.HasOne(x => x.StudyClass)
            .WithMany(x => x.ClassStudents)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

public class ClassLecturerConfiguration : IEntityTypeConfiguration<ClassLecturer>
{
    public void Configure(EntityTypeBuilder<ClassLecturer> builder)
    {
        builder.ToTable("class_lecturers");
        builder.HasKey(x => new { x.ClassId, x.LecturerId });

        builder.Property(x => x.ClassId).HasColumnName("class_id");
        builder.Property(x => x.LecturerId).HasColumnName("lecturer_id");
        builder.Property(x => x.AssignmentRole).HasColumnName("assignment_role").HasMaxLength(50).IsRequired();
        builder.Property(x => x.AssignedAt).HasColumnName("assigned_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAdd();

        builder.HasOne(x => x.StudyClass)
            .WithMany(x => x.ClassLecturers)
            .HasForeignKey(x => x.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Lecturer)
            .WithMany()
            .HasForeignKey(x => x.LecturerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
