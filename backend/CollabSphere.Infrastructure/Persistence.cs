using Microsoft.EntityFrameworkCore;
using CollabSphere.Domain.Entities;

namespace CollabSphere.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<WorkTask> Tasks => Set<WorkTask>();

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Syllabus> Syllabi => Set<Syllabus>();
    public DbSet<StudyClass> StudyClasses => Set<StudyClass>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Lecturer> Lecturers => Set<Lecturer>();
    public DbSet<ClassStudent> ClassStudents => Set<ClassStudent>();
    public DbSet<ClassLecturer> ClassLecturers => Set<ClassLecturer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
        modelBuilder.ApplyConfiguration(new WorkTaskConfiguration());
        modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        modelBuilder.ApplyConfiguration(new SyllabusConfiguration());
        modelBuilder.ApplyConfiguration(new StudyClassConfiguration());
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new LecturerConfiguration());
        modelBuilder.ApplyConfiguration(new ClassStudentConfiguration());
        modelBuilder.ApplyConfiguration(new ClassLecturerConfiguration());
    }
}
