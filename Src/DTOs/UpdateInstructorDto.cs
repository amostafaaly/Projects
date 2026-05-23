using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs;

public sealed class UpdateInstructorDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }
}
