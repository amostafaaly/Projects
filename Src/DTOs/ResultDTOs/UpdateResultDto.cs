namespace Projects.Src.DTOs.ResultDTOs
{
    public class UpdateResultDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string ExamId { get; set; } = string.Empty;
        public int Score { get; set; }
        public int TotalMarks { get; set; }
        public double Percentage { get; set; }
    }
}
