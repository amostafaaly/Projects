using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs.StudentDTOs;

public sealed class UpdateStudentDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }
    public StudentStatus Status { get; set; }
    public StudentLevel Level { get; set; }

    public string GetDetails() =>
        $"[{Level}] {Name} | ID: {Id} | Faculty: {Faculty} | Status: {Status} | Email: {Email}";
}
