using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs;

public sealed class CreateParttimeInstructorDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }
    public int HiringYear { get; set; }
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }
}
