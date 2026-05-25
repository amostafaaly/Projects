
using static Projects.Src.Shared.Enums;
namespace Projects.Src.Models;

public class Course : BaseEntity
{

    public int CreditHours {  get; set; }
    public string Description { get; set; } = string.Empty;
    public Faculty Faculty { get; set; }

    public string? InstructorId { get; set; }
    public virtual Instructor? Instructor { get; set; }

    
    
    public virtual List<StudentCourse> Enrollments { get; set; } = new List<StudentCourse>();
    public Course() { }

}