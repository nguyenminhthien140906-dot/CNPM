
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
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Lecturer> Lecturers => Set<Lecturer>();

    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Syllabus> Syllabi => Set<Syllabus>();
    public DbSet<StudyClass> StudyClasses => Set<StudyClass>();
    public DbSet<ClassStudent> ClassStudents => Set<ClassStudent>();
    public DbSet<ClassLecturer> ClassLecturers => Set<ClassLecturer>();

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    public DbSet<Milestone> Milestones => Set<Milestone>();
    public DbSet<Checkpoint> Checkpoints => Set<Checkpoint>();

    public DbSet<WorkTask> Tasks => Set<WorkTask>();
    public DbSet<TaskAssignee> TaskAssignees => Set<TaskAssignee>();
    public DbSet<Submission> Submissions => Set<Submission>();
    public DbSet<Resource> Resources => Set<Resource>();

    public DbSet<Meeting> Meetings => Set<Meeting>();
    public DbSet<MeetingParticipant> MeetingParticipants => Set<MeetingParticipant>();
    public DbSet<Message> Messages => Set<Message>();

    public DbSet<Workspace> Workspaces => Set<Workspace>();
    public DbSet<WhiteboardSnapshot> WhiteboardSnapshots => Set<WhiteboardSnapshot>();
    public DbSet<WorkspaceDocument> WorkspaceDocuments => Set<WorkspaceDocument>();
    public DbSet<DocumentVersion> DocumentVersions => Set<DocumentVersion>();

    public DbSet<Evaluation> Evaluations => Set<Evaluation>();
    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<AIConversation> AIConversations => Set<AIConversation>();
    public DbSet<AIMessage> AIMessages => Set<AIMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}
