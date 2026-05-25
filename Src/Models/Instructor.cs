using System.ComponentModel.DataAnnotations.Schema;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Models;

public abstract class Instructor : User
{
    public int HiringYear { get; set; }
    public Faculty Faculty { get; set; }

    [NotMapped]
    public InstructorStatus Status { get; set; }

    protected Instructor() { }
    protected Instructor(string id, string name, string email, Faculty faculty, int hiringYear)
    {
        Id = id;
        Name = name;
        UniversityEmail = email;
        Faculty = faculty;
        HiringYear = hiringYear;
    }

    public abstract decimal CalculateSalary();

}
public class FulltimeInstructor : Instructor
{
    public decimal MonthlySalary { get; set; }

    public FulltimeInstructor() { }

    public FulltimeInstructor(string id, string name, string email, Faculty faculty, int hiringYear, decimal monthlySalary)
        : base(id, name, email, faculty, hiringYear)
    {
        MonthlySalary = monthlySalary;
        Status = InstructorStatus.FullTime;
    }

    public override decimal CalculateSalary() => MonthlySalary;

}

public class ParttimeInstructor : Instructor
{
    public decimal HourlyRate { get; set; }
    public int HoursWorked { get; set; }

    public ParttimeInstructor() { }

    public ParttimeInstructor(string id, string name, string email, Faculty faculty, int hiringYear, decimal hourlyRate, int hoursWorked)
        : base(id, name, email, faculty, hiringYear)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
        Status = InstructorStatus.PartTime;
    }

    public override decimal CalculateSalary() => HourlyRate * HoursWorked;

}