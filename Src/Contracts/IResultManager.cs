using Projects.Src.Models;

namespace Projects.Src.Contracts
{
    public interface IResultManager : IManager<Result>
    {
        void RecordExamResult(string examId, string studentId, int score);
    }
}
