using Projects.Models.Courses;
using Projects.IServices;

namespace Projects.Interfaces
{
    public interface IResultManager : IManager<Result>
    {
        void RecordExamResult(string examId, string studentId, int score);
    }
}
