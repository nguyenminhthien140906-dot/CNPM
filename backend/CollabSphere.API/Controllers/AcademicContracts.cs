using System.ComponentModel.DataAnnotations;

namespace CollabSphere.API.Contracts;

public class SubjectRequest
{
    [Required, StringLength(30)]
    public string SubjectCode { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string SubjectName { get; set; } = string.Empty;

    public string? Description { get; set; }
}

public class SyllabusRequest
{
    [Required]
    public long SubjectId { get; set; }

    [Required, StringLength(30)]
    public string Version { get; set; } = string.Empty;

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }
}

public class StudyClassRequest
{
    [Required]
    public long SubjectId { get; set; }

    [Required, StringLength(30)]
    public string ClassCode { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string ClassName { get; set; } = string.Empty;

    [StringLength(30)]
    public string? Semester { get; set; }

    [StringLength(20)]
    public string? AcademicYear { get; set; }

    [Range(0, int.MaxValue)]
    public int? Capacity { get; set; }

    [RegularExpression("^(PLANNED|OPEN|CLOSED|ARCHIVED)$",
        ErrorMessage = "Status phải là PLANNED, OPEN, CLOSED hoặc ARCHIVED.")]
    public string Status { get; set; } = "OPEN";
}

public class StudyClassPatchRequest
{
    public long? SubjectId { get; set; }

    [StringLength(30, MinimumLength = 1)]
    public string? ClassCode { get; set; }

    [StringLength(150, MinimumLength = 1)]
    public string? ClassName { get; set; }

    [StringLength(30)]
    public string? Semester { get; set; }

    [StringLength(20)]
    public string? AcademicYear { get; set; }

    [Range(0, int.MaxValue)]
    public int? Capacity { get; set; }

    [RegularExpression("^(PLANNED|OPEN|CLOSED|ARCHIVED)$",
        ErrorMessage = "Status phải là PLANNED, OPEN, CLOSED hoặc ARCHIVED.")]
    public string? Status { get; set; }
}

public class StudyClassStatusRequest
{
    [Required]
    [RegularExpression("^(PLANNED|OPEN|CLOSED|ARCHIVED)$",
        ErrorMessage = "Status phải là PLANNED, OPEN, CLOSED hoặc ARCHIVED.")]
    public string Status { get; set; } = string.Empty;
}

public class AssignLecturerRequest
{
    [Required]
    public long LecturerId { get; set; }

    [StringLength(50)]
    public string AssignmentRole { get; set; } = "LECTURER";
}

public class EnrollStudentRequest
{
    [Required]
    public long StudentId { get; set; }
}
