using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs.StudentDTOs;

public sealed class CreateStudentDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }
    public int EnrollmentYear { get; set; }
}
