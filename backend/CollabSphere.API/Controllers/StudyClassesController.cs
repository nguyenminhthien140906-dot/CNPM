using CollabSphere.API.Contracts;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[Route("api/classes")]
[Authorize]
public class StudyClassesController : ApiControllerBase
{
    private const string ManageRoles = "ADMIN,HEAD,STAFF";
    private readonly ApplicationDbContext _context;

    public StudyClassesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] long? subjectId, [FromQuery] string? status)
    {
        var query = _context.StudyClasses.AsNoTracking();

        if (subjectId.HasValue)
            query = query.Where(c => c.SubjectId == subjectId.Value);

        if (!string.IsNullOrWhiteSpace(status))
        {
            var st = status.Trim().ToUpper();
            query = query.Where(c => c.Status == st);
        }

        var data = await query
            .OrderBy(c => c.ClassCode)
            .Select(c => new
            {
                c.ClassId,
                c.ClassCode,
                c.ClassName,
                c.SubjectId,
                SubjectCode = c.Subject!.SubjectCode,
                SubjectName = c.Subject.SubjectName,
                c.Semester,
                c.AcademicYear,
                c.Capacity,
                c.Status,
                StudentCount = c.ClassStudents.Count,
                LecturerCount = c.ClassLecturers.Count,
                c.CreatedAt
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _context.StudyClasses.AsNoTracking()
            .Where(c => c.ClassId == id)
            .Select(c => new
            {
                c.ClassId,
                c.ClassCode,
                c.ClassName,
                c.SubjectId,
                SubjectCode = c.Subject!.SubjectCode,
                SubjectName = c.Subject.SubjectName,
                c.Semester,
                c.AcademicYear,
                c.Capacity,
                c.Status,
                StudentCount = c.ClassStudents.Count,
                LecturerCount = c.ClassLecturers.Count,
                c.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (data == null)
            return Failure(404, "Không tìm thấy lớp học.");

        return Success(data);
    }

    [HttpPost]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Create([FromBody] StudyClassRequest request)
    {
        if (!await _context.Subjects.AnyAsync(s => s.SubjectId == request.SubjectId))
            return Failure(404, "Không tìm thấy môn học.");

        var code = request.ClassCode.Trim();

        if (await _context.StudyClasses.AnyAsync(c => c.ClassCode == code))
            return Failure(409, "Mã lớp đã tồn tại.");

        var studyClass = new StudyClass
        {
            SubjectId = request.SubjectId,
            ClassCode = code,
            ClassName = request.ClassName.Trim(),
            Semester = request.Semester,
            AcademicYear = request.AcademicYear,
            Capacity = request.Capacity,
            Status = request.Status
        };

        _context.StudyClasses.Add(studyClass);
        await _context.SaveChangesAsync();

        return CreatedOk(new { studyClass.ClassId, studyClass.ClassCode, studyClass.ClassName });
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Update(long id, [FromBody] StudyClassRequest request)
    {
        var studyClass = await _context.StudyClasses.FirstOrDefaultAsync(c => c.ClassId == id);

        if (studyClass == null)
            return Failure(404, "Không tìm thấy lớp học.");

        if (!await _context.Subjects.AnyAsync(s => s.SubjectId == request.SubjectId))
            return Failure(404, "Không tìm thấy môn học.");

        var code = request.ClassCode.Trim();

        if (await _context.StudyClasses.AnyAsync(c => c.ClassCode == code && c.ClassId != id))
            return Failure(409, "Mã lớp đã tồn tại.");

        if (request.Capacity.HasValue)
        {
            var studentCount = await _context.ClassStudents.CountAsync(cs => cs.ClassId == id);

            if (request.Capacity.Value < studentCount)
                return Failure(409, $"Sĩ số tối đa không được nhỏ hơn số sinh viên hiện tại ({studentCount}).");
        }

        studyClass.SubjectId = request.SubjectId;
        studyClass.ClassCode = code;
        studyClass.ClassName = request.ClassName.Trim();
        studyClass.Semester = request.Semester;
        studyClass.AcademicYear = request.AcademicYear;
        studyClass.Capacity = request.Capacity;
        studyClass.Status = request.Status;

        await _context.SaveChangesAsync();

        return Success(new { studyClass.ClassId, studyClass.ClassCode, studyClass.ClassName }, "Cập nhật thành công.");
    }

    [HttpPatch("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Patch(long id, [FromBody] StudyClassPatchRequest request)
    {
        var studyClass = await _context.StudyClasses.FirstOrDefaultAsync(c => c.ClassId == id);

        if (studyClass == null)
            return Failure(404, "Không tìm thấy lớp học.");

        if (request.SubjectId.HasValue)
        {
            if (!await _context.Subjects.AnyAsync(s => s.SubjectId == request.SubjectId.Value))
                return Failure(404, "Không tìm thấy môn học.");

            studyClass.SubjectId = request.SubjectId.Value;
        }

        if (request.ClassCode != null)
        {
            var code = request.ClassCode.Trim();

            if (await _context.StudyClasses.AnyAsync(c => c.ClassCode == code && c.ClassId != id))
                return Failure(409, "Mã lớp đã tồn tại.");

            studyClass.ClassCode = code;
        }

        if (request.ClassName != null)
            studyClass.ClassName = request.ClassName.Trim();

        if (request.Semester != null)
            studyClass.Semester = request.Semester;

        if (request.AcademicYear != null)
            studyClass.AcademicYear = request.AcademicYear;

        if (request.Capacity.HasValue)
        {
            var studentCount = await _context.ClassStudents.CountAsync(cs => cs.ClassId == id);

            if (request.Capacity.Value < studentCount)
                return Failure(409, $"Sĩ số tối đa không được nhỏ hơn số sinh viên hiện tại ({studentCount}).");

            studyClass.Capacity = request.Capacity.Value;
        }

        if (request.Status != null)
            studyClass.Status = request.Status;

        await _context.SaveChangesAsync();

        return Success(new { studyClass.ClassId, studyClass.ClassCode, studyClass.ClassName, studyClass.Status },
            "Cập nhật thành công.");
    }

    [HttpPatch("{id:long}/status")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> UpdateStatus(long id, [FromBody] StudyClassStatusRequest request)
    {
        var studyClass = await _context.StudyClasses.FirstOrDefaultAsync(c => c.ClassId == id);

        if (studyClass == null)
            return Failure(404, "Không tìm thấy lớp học.");

        studyClass.Status = request.Status;
        await _context.SaveChangesAsync();

        return Success(new { studyClass.ClassId, studyClass.ClassCode, studyClass.Status },
            "Cập nhật trạng thái thành công.");
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Delete(long id)
    {
        var studyClass = await _context.StudyClasses.FirstOrDefaultAsync(c => c.ClassId == id);

        if (studyClass == null)
            return Failure(404, "Không tìm thấy lớp học.");

        if (await _context.Projects.AnyAsync(p => p.ClassId == id))
            return Failure(409, "Lớp đang có dự án, không thể xóa.");

        _context.StudyClasses.Remove(studyClass);
        await _context.SaveChangesAsync();

        return Success<object?>(null, "Xóa thành công.");
    }
}
