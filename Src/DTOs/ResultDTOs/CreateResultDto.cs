namespace Projects.Src.DTOs.ResultDTOs
{
    public class CreateResultDto
    {
        public string StudentId { get; set; } = string.Empty;
        public string ExamId { get; set; } = string.Empty;
        public int Score { get; set; }
    }
}
