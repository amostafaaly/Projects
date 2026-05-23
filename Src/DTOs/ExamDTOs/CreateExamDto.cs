namespace Projects.Src.DTOs.ExamDTOs
{
    public class CreateExamDTO
    {
        public string CourseId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int TotalMarks { get; set; }
    }
}
