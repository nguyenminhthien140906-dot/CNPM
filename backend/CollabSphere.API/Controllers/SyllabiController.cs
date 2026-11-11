using CollabSphere.API.Contracts;
using CollabSphere.Domain.Entities;
using CollabSphere.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CollabSphere.API.Controllers;

[Route("api/syllabi")]
[Authorize]
public class SyllabiController : ApiControllerBase
{
    private const string ManageRoles = "ADMIN,HEAD,STAFF";
    private readonly ApplicationDbContext _context;

    public SyllabiController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] long? subjectId)
    {
        var query = _context.Syllabi.AsNoTracking();

        if (subjectId.HasValue)
            query = query.Where(s => s.SubjectId == subjectId.Value);

        var data = await query
            .OrderBy(s => s.SubjectId).ThenBy(s => s.Version)
            .Select(s => new
            {
                s.SyllabusId,
                s.SubjectId,
                SubjectCode = s.Subject!.SubjectCode,
                SubjectName = s.Subject.SubjectName,
                s.Version,
                s.Title,
                s.CreatedAt
            })
            .ToListAsync();

        return Success(data);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id)
    {
        var data = await _context.Syllabi.AsNoTracking()
            .Where(s => s.SyllabusId == id)
            .Select(s => new
            {
                s.SyllabusId,
                s.SubjectId,
                SubjectCode = s.Subject!.SubjectCode,
                SubjectName = s.Subject.SubjectName,
                s.Version,
                s.Title,
                s.Content,
                s.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (data == null)
            return Failure(404, "Không tìm thấy syllabus.");

        return Success(data);
    }

    [HttpPost]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Create([FromBody] SyllabusRequest request)
    {
        if (!await _context.Subjects.AnyAsync(s => s.SubjectId == request.SubjectId))
            return Failure(404, "Không tìm thấy môn học.");

        var version = request.Version.Trim();

        if (await _context.Syllabi.AnyAsync(s => s.SubjectId == request.SubjectId && s.Version == version))
            return Failure(409, "Môn học này đã có syllabus với phiên bản đó.");

        var syllabus = new Syllabus
        {
            SubjectId = request.SubjectId,
            Version = version,
            Title = request.Title.Trim(),
            Content = request.Content
        };

        _context.Syllabi.Add(syllabus);
        await _context.SaveChangesAsync();

        return CreatedOk(new { syllabus.SyllabusId, syllabus.SubjectId, syllabus.Version, syllabus.Title });
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Update(long id, [FromBody] SyllabusRequest request)
    {
        var syllabus = await _context.Syllabi.FirstOrDefaultAsync(s => s.SyllabusId == id);

        if (syllabus == null)
            return Failure(404, "Không tìm thấy syllabus.");

        if (!await _context.Subjects.AnyAsync(s => s.SubjectId == request.SubjectId))
            return Failure(404, "Không tìm thấy môn học.");

        var version = request.Version.Trim();

        if (await _context.Syllabi.AnyAsync(s =>
                s.SubjectId == request.SubjectId && s.Version == version && s.SyllabusId != id))
            return Failure(409, "Môn học này đã có syllabus với phiên bản đó.");

        syllabus.SubjectId = request.SubjectId;
        syllabus.Version = version;
        syllabus.Title = request.Title.Trim();
        syllabus.Content = request.Content;

        await _context.SaveChangesAsync();

        return Success(new { syllabus.SyllabusId, syllabus.SubjectId, syllabus.Version, syllabus.Title },
            "Cập nhật thành công.");
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = ManageRoles)]
    public async Task<IActionResult> Delete(long id)
    {
        var syllabus = await _context.Syllabi.FirstOrDefaultAsync(s => s.SyllabusId == id);

        if (syllabus == null)
            return Failure(404, "Không tìm thấy syllabus.");

        if (await _context.Projects.AnyAsync(p => p.SyllabusId == id))
            return Failure(409, "Syllabus đang được dự án sử dụng, không thể xóa.");

        _context.Syllabi.Remove(syllabus);
        await _context.SaveChangesAsync();

        return Success<object?>(null, "Xóa thành công.");
    }
}
