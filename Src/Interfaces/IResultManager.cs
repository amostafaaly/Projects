using Projects.Src.Models.Courses;

namespace Projects.Src.Interfaces
{
    public interface IResultManager : IManager<Result>
    {
        void RecordExamResult(string examId, string studentId, int score);
    }
}
