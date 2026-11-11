using CollabSphere.API.Contracts;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[Route("api/subjects")]
[Authorize]
public class SubjectsController : ApiControllerBase
{
    private const string ManageRoles = "ADMIN,HEAD,STAFF";
    private readonly ApplicationDbContext _context;

    public SubjectsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? keyword)
    {
        var query = _context.Subjects.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var k = keyword.Trim();
            query = query.Where(s => s.SubjectCode.Contains(k) || s.SubjectName.Contains(k));
        }

        var data = await query
            .OrderBy(s => s.SubjectCode)
            .Select(s => new
            {
                s.SubjectId,
                s.SubjectCode,
                s.SubjectName,
                s.Description,
                s.CreatedAt,
                SyllabusCount = s.Syllabi.Count,
                ClassCount = s.Classes.Count
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _context.Subjects.AsNoTracking()
            .Where(s => s.SubjectId == id)
            .Select(s => new
            {
                s.SubjectId,
                s.SubjectCode,
                s.SubjectName,
                s.Description,
                s.CreatedAt,
                SyllabusCount = s.Syllabi.Count,
                ClassCount = s.Classes.Count
            })
            .FirstOrDefaultAsync();

        if (data == null)
            return Failure(404, "Không tìm thấy môn học.");

        return Success(data);
    }

    [HttpPost]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Create([FromBody] SubjectRequest request)
    {
        var code = request.SubjectCode.Trim();

        if (await _context.Subjects.AnyAsync(s => s.SubjectCode == code))
            return Failure(409, "Mã môn học đã tồn tại.");

        var subject = new Subject
        {
            SubjectCode = code,
            SubjectName = request.SubjectName.Trim(),
            Description = request.Description
        };

        _context.Subjects.Add(subject);
        await _context.SaveChangesAsync();

        return CreatedOk(new { subject.SubjectId, subject.SubjectCode, subject.SubjectName });
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Update(long id, [FromBody] SubjectRequest request)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == id);

        if (subject == null)
            return Failure(404, "Không tìm thấy môn học.");

        var code = request.SubjectCode.Trim();

        if (await _context.Subjects.AnyAsync(s => s.SubjectCode == code && s.SubjectId != id))
            return Failure(409, "Mã môn học đã tồn tại.");

        subject.SubjectCode = code;
        subject.SubjectName = request.SubjectName.Trim();
        subject.Description = request.Description;

        await _context.SaveChangesAsync();

        return Success(new { subject.SubjectId, subject.SubjectCode, subject.SubjectName }, "Cập nhật thành công.");
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Delete(long id)
    {
        var subject = await _context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == id);

        if (subject == null)
            return Failure(404, "Không tìm thấy môn học.");

        if (await _context.StudyClasses.AnyAsync(c => c.SubjectId == id))
            return Failure(409, "Môn học đang có lớp, không thể xóa.");

        _context.Subjects.Remove(subject);
        await _context.SaveChangesAsync();

        return Success<object?>(null, "Xóa thành công.");
    }
}
