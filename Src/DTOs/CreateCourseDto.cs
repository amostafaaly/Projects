using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs;

public sealed class CreateCourseDto
{
    public string Name { get; set; } = string.Empty;
    public int CreditHours { get; set; }
    public string Description { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }
}
