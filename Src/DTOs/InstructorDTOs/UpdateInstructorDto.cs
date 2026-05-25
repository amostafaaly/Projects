using static Projects.Src.Shared.Enums;

namespace Projects.Src.DTOs.InstructorDTOs;

public abstract class UpdateInstructorDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }

    public abstract string GetDetails();
}

public class FulltimeInstructorUpdateDto : UpdateInstructorDto
{
    public decimal MonthlySalary { get; set; }
    public override string GetDetails() =>
        $"[Full-Time] {Name} | ID: {Id} | Faculty: {Faculty} | Monthly Salary: ${MonthlySalary:F2}";
}

public class ParttimeInstructorUpdateDto : UpdateInstructorDto
{
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }
    public decimal CalculatedSalary => HourlyRate * HoursWorked;

    public override string GetDetails() =>
        $"[Part-Time] {Name} | ID: {Id} | Faculty: {Faculty} | Rate: ${HourlyRate}/hr × {HoursWorked}hrs = ${CalculatedSalary:F2}";
}
