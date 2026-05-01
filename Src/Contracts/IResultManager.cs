using Projects.Src.Models;

namespace Projects.Src.Contracts
{
    public interface IResultManager : IManager<Result>
    {
        public List<Result> GetResultByStudentid(string studentid);
    }
}
