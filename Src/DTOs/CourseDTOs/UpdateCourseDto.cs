using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs.CourseDTOs;

public sealed class UpdateCourseDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public string Description { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }
}
