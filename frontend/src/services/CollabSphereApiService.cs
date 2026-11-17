using System.Net.Http.Json;

namespace CollabSphere.Frontend.Services
{
    // === MODELS / DTOs ===
    public class UserDto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N")[..6].ToUpper();
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "Staff"; // Admin, Head, Staff, Lecturer, Student
        public bool IsActive { get; set; } = true;
    }

    public class SubjectDto
    {
        public string Id { get; set; } = "";
        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public int Credits { get; set; } = 3;
        public string Department { get; set; } = "Công nghệ phần mềm";
    }

    public class SyllabusDto
    {
        public string Id { get; set; } = "";
        public string SubjectCode { get; set; } = "";
        public string SubjectName { get; set; } = "";
        public string Version { get; set; } = "1.0";
        public string Description { get; set; } = "";
        public string Status { get; set; } = "Chờ duyệt"; // Đã duyệt, Chờ duyệt, Nháp
    }

    public class ClassDto
    {
        public string Id { get; set; } = "";
        public string ClassCode { get; set; } = "";
        public string SubjectCode { get; set; } = "";
        public string LecturerName { get; set; } = "";
        public string Semester { get; set; } = "HK1-2026";
        public int StudentCount { get; set; }
    }

    public class ProjectDto
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string GroupName { get; set; } = "";
        public string LecturerName { get; set; } = "";
        public string Status { get; set; } = "Chờ duyệt"; // Chờ duyệt, Đã duyệt, Từ chối
        public string Note { get; set; } = "";
    }

    // === API SERVICE ===
    public class CollabSphereApiService
    {
        private readonly HttpClient _http;

        public CollabSphereApiService(HttpClient http)
        {
            _http = http;
        }

        // 1. USERS (Admin)
        public async Task<List<UserDto>> GetUsersAsync()
        {
            try
            {
                var res = await _http.GetFromJsonAsync<List<UserDto>>("api/users");
                return res ?? GetMockUsers();
            }
            catch { return GetMockUsers(); }
        }

        public async Task<bool> CreateUserAsync(UserDto user)
        {
            try { return (await _http.PostAsJsonAsync("api/users", user)).IsSuccessStatusCode; }
            catch { return true; }
        }

        public async Task<bool> UpdateUserAsync(UserDto user)
        {
            try { return (await _http.PutAsJsonAsync($"api/users/{user.Id}", user)).IsSuccessStatusCode; }
            catch { return true; }
        }

        public async Task<bool> ToggleUserStatusAsync(string id, bool isActive)
        {
            try { return (await _http.PatchAsJsonAsync($"api/users/{id}/status", new { isActive })).IsSuccessStatusCode; }
            catch { return true; }
        }

        // 2. SUBJECTS (Môn học)
        public async Task<List<SubjectDto>> GetSubjectsAsync()
        {
            try
            {
                var res = await _http.GetFromJsonAsync<List<SubjectDto>>("api/subjects");
                return res ?? GetMockSubjects();
            }
            catch { return GetMockSubjects(); }
        }

        public async Task<bool> SaveSubjectAsync(SubjectDto subject, bool isNew)
        {
            try
            {
                var resp = isNew
                    ? await _http.PostAsJsonAsync("api/subjects", subject)
                    : await _http.PutAsJsonAsync($"api/subjects/{subject.Id}", subject);
                return resp.IsSuccessStatusCode;
            }
            catch { return true; }
        }

        public async Task<bool> DeleteSubjectAsync(string id)
        {
            try { return (await _http.DeleteAsync($"api/subjects/{id}")).IsSuccessStatusCode; }
            catch { return true; }
        }

        // 3. SYLLABUS (Đề cương)
        public async Task<List<SyllabusDto>> GetSyllabusesAsync()
        {
            try
            {
                var res = await _http.GetFromJsonAsync<List<SyllabusDto>>("api/syllabuses");
                return res ?? GetMockSyllabuses();
            }
            catch { return GetMockSyllabuses(); }
        }

        public async Task<bool> SaveSyllabusAsync(SyllabusDto syllabus, bool isNew)
        {
            try
            {
                var resp = isNew
                    ? await _http.PostAsJsonAsync("api/syllabuses", syllabus)
                    : await _http.PutAsJsonAsync($"api/syllabuses/{syllabus.Id}", syllabus);
                return resp.IsSuccessStatusCode;
            }
            catch { return true; }
        }

        public async Task<bool> DeleteSyllabusAsync(string id)
        {
            try { return (await _http.DeleteAsync($"api/syllabuses/{id}")).IsSuccessStatusCode; }
            catch { return true; }
        }

        // 4. CLASSES (Lớp học - Staff)
        public async Task<List<ClassDto>> GetClassesAsync()
        {
            try
            {
                var res = await _http.GetFromJsonAsync<List<ClassDto>>("api/classes");
                return res ?? GetMockClasses();
            }
            catch { return GetMockClasses(); }
        }

        public async Task<bool> SaveClassAsync(ClassDto cls, bool isNew)
        {
            try
            {
                var resp = isNew
                    ? await _http.PostAsJsonAsync("api/classes", cls)
                    : await _http.PutAsJsonAsync($"api/classes/{cls.Id}", cls);
                return resp.IsSuccessStatusCode;
            }
            catch { return true; }
        }

        public async Task<bool> DeleteClassAsync(string id)
        {
            try { return (await _http.DeleteAsync($"api/classes/{id}")).IsSuccessStatusCode; }
            catch { return true; }
        }

        // 5. PROJECTS (Duyệt dự án - Head Department)
        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            try
            {
                var res = await _http.GetFromJsonAsync<List<ProjectDto>>("api/projects");
                return res ?? GetMockProjects();
            }
            catch { return GetMockProjects(); }
        }

        public async Task<bool> UpdateProjectStatusAsync(string id, string status, string note = "")
        {
            try { return (await _http.PatchAsJsonAsync($"api/projects/{id}/status", new { status, note })).IsSuccessStatusCode; }
            catch { return true; }
        }

        // === DỮ LIỆU MẪU KHI CHƯA CÓ API ===
        private List<UserDto> GetMockUsers() => new()
        {
            new UserDto { Id = "U001", FullName = "Nguyễn Văn An", Email = "an.nv@ut.edu.vn", Role = "Admin", IsActive = true },
            new UserDto { Id = "U002", FullName = "Trần Thị Bình", Email = "binh.tt@ut.edu.vn", Role = "Head", IsActive = true },
            new UserDto { Id = "U003", FullName = "Lê Hoàng Cường", Email = "cuong.lh@ut.edu.vn", Role = "Staff", IsActive = true },
            new UserDto { Id = "U004", FullName = "Phạm Minh Dung", Email = "dung.pm@ut.edu.vn", Role = "Lecturer", IsActive = false }
        };

        private List<SubjectDto> GetMockSubjects() => new()
        {
            new SubjectDto { Id = "S01", Code = "SE104", Name = "Nhập môn Công nghệ phần mềm", Credits = 4, Department = "CNPM" },
            new SubjectDto { Id = "S02", Code = "CS112", Name = "Phân tích và thiết kế thuật toán", Credits = 4, Department = "KHMT" },
            new SubjectDto { Id = "S03", Code = "IS201", Name = "Phân tích thiết kế hệ thống thông tin", Credits = 4, Department = "HTTT" }
        };

        private List<SyllabusDto> GetMockSyllabuses() => new()
        {
            new SyllabusDto { Id = "SYL01", SubjectCode = "SE104", SubjectName = "Nhập môn Công nghệ phần mềm", Version = "2026.1", Description = "Quy trình phát triển phần mềm Agile/Scrum", Status = "Đã duyệt" },
            new SyllabusDto { Id = "SYL02", SubjectCode = "CS112", SubjectName = "Phân tích và thiết kế thuật toán", Version = "2026.1", Description = "Quy hoạch động, tham lam, đồ thị", Status = "Chờ duyệt" }
        };

        private List<ClassDto> GetMockClasses() => new()
        {
            new ClassDto { Id = "C01", ClassCode = "SE104.O21", SubjectCode = "SE104", LecturerName = "ThS. Nguyễn Văn A", Semester = "HK1-2026", StudentCount = 45 },
            new ClassDto { Id = "C02", ClassCode = "CS112.O22", SubjectCode = "CS112", LecturerName = "TS. Lê Thị B", Semester = "HK1-2026", StudentCount = 50 }
        };

        private List<ProjectDto> GetMockProjects() => new()
        {
            new ProjectDto { Id = "PRJ01", Name = "Hệ thống quản lý học tập CollabSphere", GroupName = "Nhóm 01 - SE104.O21", LecturerName = "ThS. Nguyễn Văn A", Status = "Chờ duyệt" },
            new ProjectDto { Id = "PRJ02", Name = "Ứng dụng đặt lịch phòng khám", GroupName = "Nhóm 04 - SE104.O21", LecturerName = "ThS. Nguyễn Văn A", Status = "Đã duyệt" }
        };
    }
}
