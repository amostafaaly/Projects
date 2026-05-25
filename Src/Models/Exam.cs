namespace Projects.Src.Models;

public class Exam : BaseEntity
{
    public DateTime Date { get; set; }
    public int TotalMarks { get; set; }
    
    public string? CourseId { get; set; }
    public virtual Course? Course { get; set; }

    public Exam(){}

}