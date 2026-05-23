using Projects.Src.DTOs.ExamDTOs;

namespace Projects.Src.Interfaces
{
    public interface IExamService
    {
        void CreateExam(CreateExamDTO dto);
        IEnumerable<UpdateExamDto> GetAllExams();
        UpdateExamDto GetExamById(string id);
        void UpdateExam(UpdateExamDto dto);
        void DeleteExam(string id);
    }
}
