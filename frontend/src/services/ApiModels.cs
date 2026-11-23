namespace CollabSphere.Frontend.Services
{
    public class ApiResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";

        public static ApiResult Ok(string message = "Thành công") => new() { Success = true, Message = message };
        public static ApiResult Fail(string message) => new() { Success = false, Message = message };
    }

    // 1. AdminUsers.razor
    public class UserDto
    {
        public string Id { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "STUDENT"; // ADMIN, HEAD, STAFF, LECTURER, STUDENT
        public string Status { get; set; } = "Hoạt động"; // Hoạt động, Bị khóa
    }

    // 2. Dashboard.razor
    public class DashboardSummaryDto
    {
        public int TotalProjects { get; set; }
        public int TotalClasses { get; set; }
        public int TotalTeams { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int InProgressTasks { get; set; }
        public int PendingTasks { get; set; }
        public List<DashboardProjectDto> RecentProjects { get; set; } = new();
        public List<DashboardCheckpointDto> UpcomingCheckpoints { get; set; } = new();
    }

    public class DashboardProjectDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "";
    }

    public class DashboardCheckpointDto
    {
        public string Name { get; set; } = "";
        public string Project { get; set; } = "";
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = "";
    }

    // 3. Subjects.razor
    public class SubjectDto
    {
        public string SubjectCode { get; set; } = "";
        public string SubjectName { get; set; } = "";
        public int Credits { get; set; } = 3;
        public string Description { get; set; } = "";
        public string Status { get; set; } = "ACTIVE"; // ACTIVE, INACTIVE
    }

    // 4. Syllabus.razor
    public class SyllabusDto
    {
        public string SyllabusCode { get; set; } = "";
        public string SubjectCode { get; set; } = "";
        public string SubjectName { get; set; } = "";
        public string Version { get; set; } = "2026.1";
        public string Objectives { get; set; } = "";
        public string ContentSummary { get; set; } = "";
        public string Status { get; set; } = "DRAFT"; // DRAFT, PENDING, APPROVED
    }

    // 5. StaffClasses.razor
    public class ClassDto
    {
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public string Semester { get; set; } = "1"; // 1, 2, 3
        public string AcademicYear { get; set; } = "2026-2027";
        public int Capacity { get; set; } = 40;
        public int StudentCount { get; set; }
        public string Status { get; set; } = "ACTIVE"; // ACTIVE, INACTIVE
    }

    // 6. HeadApproval.razor
    public class ProjectApprovalDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Lecturer { get; set; } = "";
        public string SubmittedDate { get; set; } = "";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "PENDING"; // PENDING, APPROVED, REJECTED
        public string RejectionReason { get; set; } = "";
    }
}
