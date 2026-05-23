namespace Projects.Src.DTOs.ExamDTOs
{
    public class UpdateExamDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int TotalMarks { get; set; }
    }
}
