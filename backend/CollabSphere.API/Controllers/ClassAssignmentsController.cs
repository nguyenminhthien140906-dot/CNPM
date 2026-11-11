using CollabSphere.API.Contracts;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[Route("api")]
[Authorize]
public class ClassAssignmentsController : ApiControllerBase
{
    private const string ManageRoles = "ADMIN,HEAD,STAFF";
    private readonly ApplicationDbContext _context;

    public ClassAssignmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("lecturers")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> GetLecturers()
    {
        var data = await _context.Lecturers.AsNoTracking()
            .OrderBy(l => l.LecturerCode)
            .Select(l => new
            {
                l.LecturerId,
                l.LecturerCode,
                l.User!.FullName,
                l.User.Email
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpGet("students")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> GetStudents()
    {
        var data = await _context.Students.AsNoTracking()
            .OrderBy(s => s.StudentCode)
            .Select(s => new
            {
                s.StudentId,
                s.StudentCode,
                s.User!.FullName,
                s.User.Email
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpGet("classes/{classId:long}/lecturers")]
    public async Task<IActionResult> GetClassLecturers(long classId)
    {
        if (!await _context.StudyClasses.AnyAsync(c => c.ClassId == classId))
            return Failure(404, "Không tìm thấy lớp học.");

        var data = await _context.ClassLecturers.AsNoTracking()
            .Where(cl => cl.ClassId == classId)
            .Select(cl => new
            {
                cl.LecturerId,
                cl.Lecturer!.LecturerCode,
                cl.Lecturer.User!.FullName,
                cl.Lecturer.User.Email,
                cl.AssignmentRole,
                cl.AssignedAt
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpPost("classes/{classId:long}/lecturers")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> AssignLecturer(long classId, [FromBody] AssignLecturerRequest request)
    {
        if (!await _context.StudyClasses.AnyAsync(c => c.ClassId == classId))
            return Failure(404, "Không tìm thấy lớp học.");

        if (!await _context.Lecturers.AnyAsync(l => l.LecturerId == request.LecturerId))
            return Failure(404, "Không tìm thấy giảng viên.");

        if (await _context.ClassLecturers.AnyAsync(cl =>
                cl.ClassId == classId && cl.LecturerId == request.LecturerId))
            return Failure(409, "Giảng viên đã được phân công vào lớp này.");

        var assignment = new ClassLecturer
        {
            ClassId = classId,
            LecturerId = request.LecturerId,
            AssignmentRole = string.IsNullOrWhiteSpace(request.AssignmentRole)
                ? "LECTURER"
                : request.AssignmentRole.Trim()
        };

        _context.ClassLecturers.Add(assignment);
        await _context.SaveChangesAsync();

        return CreatedOk(new { assignment.ClassId, assignment.LecturerId, assignment.AssignmentRole },
            "Phân công giảng viên thành công.");
    }

    [HttpDelete("classes/{classId:long}/lecturers/{lecturerId:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> RemoveLecturer(long classId, long lecturerId)
    {
        var assignment = await _context.ClassLecturers
            .FirstOrDefaultAsync(cl => cl.ClassId == classId && cl.LecturerId == lecturerId);

        if (assignment == null)
            return Failure(404, "Giảng viên chưa được phân công vào lớp này.");

        _context.ClassLecturers.Remove(assignment);
        await _context.SaveChangesAsync();

        return Success<object?>(null, "Đã gỡ giảng viên khỏi lớp.");
    }

    [HttpGet("classes/{classId:long}/students")]
    public async Task<IActionResult> GetClassStudents(long classId)
    {
        if (!await _context.StudyClasses.AnyAsync(c => c.ClassId == classId))
            return Failure(404, "Không tìm thấy lớp học.");

        var data = await _context.ClassStudents.AsNoTracking()
            .Where(cs => cs.ClassId == classId)
            .Select(cs => new
            {
                cs.StudentId,
                cs.Student!.StudentCode,
                cs.Student.User!.FullName,
                cs.Student.User.Email,
                cs.EnrolledAt
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpPost("classes/{classId:long}/students")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> EnrollStudent(long classId, [FromBody] EnrollStudentRequest request)
    {
        var studyClass = await _context.StudyClasses.AsNoTracking()
            .FirstOrDefaultAsync(c => c.ClassId == classId);

        if (studyClass == null)
            return Failure(404, "Không tìm thấy lớp học.");

        if (studyClass.Status == "CLOSED" || studyClass.Status == "ARCHIVED")
            return Failure(409, "Lớp đã đóng, không thể thêm sinh viên.");

        if (!await _context.Students.AnyAsync(s => s.StudentId == request.StudentId))
            return Failure(404, "Không tìm thấy sinh viên.");

        if (await _context.ClassStudents.AnyAsync(cs =>
                cs.ClassId == classId && cs.StudentId == request.StudentId))
            return Failure(409, "Sinh viên đã có trong lớp này.");

        if (studyClass.Capacity.HasValue)
        {
            var current = await _context.ClassStudents.CountAsync(cs => cs.ClassId == classId);

            if (current >= studyClass.Capacity.Value)
                return Failure(409, "Lớp đã đủ sĩ số.");
        }

        var enrollment = new ClassStudent
        {
            ClassId = classId,
            StudentId = request.StudentId
        };

        _context.ClassStudents.Add(enrollment);
        await _context.SaveChangesAsync();

        return CreatedOk(new { enrollment.ClassId, enrollment.StudentId }, "Thêm sinh viên vào lớp thành công.");
    }

    [HttpDelete("classes/{classId:long}/students/{studentId:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> RemoveStudent(long classId, long studentId)
    {
        var enrollment = await _context.ClassStudents
            .FirstOrDefaultAsync(cs => cs.ClassId == classId && cs.StudentId == studentId);

        if (enrollment == null)
            return Failure(404, "Sinh viên chưa có trong lớp này.");

        _context.ClassStudents.Remove(enrollment);
        await _context.SaveChangesAsync();

        return Success<object?>(null, "Đã xóa sinh viên khỏi lớp.");
    }
}
