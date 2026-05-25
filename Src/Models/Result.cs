namespace Projects.Src.Models;

public class Result : BaseEntity
{    
    public string StudentId { get; set; } = string.Empty;
    public virtual Student? Student { get; set; }

    public string ExamId { get; set; } = string.Empty;
    public virtual Exam? Exam { get; set; }

    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public double Percentage => TotalMarks > 0 ? Math.Round((double)Score / TotalMarks * 100, 2) : 0;


    public Result() { }

}