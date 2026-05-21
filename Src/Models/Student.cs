using static Projects.Src.Shared.Enums;

namespace Projects.Src.Models;

public class Student : User
{
    public int EnrollmentYear { get; set; }
    public Faculty Faculty { get; set; }

    public StudentStatus Status { get; set; }
    public StudentLevel Level { get; set; }


    public virtual List<StudentCourse> Enrollments { get; set; } = new List<StudentCourse>();
    public virtual List<Result> Results { get; set; } = new List<Result>();
    public Student() { }

    public Student(string firstName, string lastName, Faculty faculty, int year, string id, string email)
    {
        Id = id;
        Name = $"{firstName} {lastName}";
        UniversityEmail = email;
        EnrollmentYear = year;
        Faculty = faculty;
        Status = StudentStatus.Active;
        Level = StudentLevel.Undergraduate;
    }

    public override string GetDetails()
             => $"[{Level}] {Name} | ID: {Id} | Faculty: {Faculty} | Status: {Status} | Email: {UniversityEmail}";
}