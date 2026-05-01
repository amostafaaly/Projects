using Projects.Src.Models;

namespace Projects.Src.Contracts.IManger
{
    public interface IResultManager : IManager<Result>
    {
        public List<Result> GetResultByStudentid(string studentid);
    }
}
