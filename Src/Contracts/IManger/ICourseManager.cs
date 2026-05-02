using Projects.Src.Models;

namespace Projects.Src.Contracts.IManger
{
    public interface ICourseManager : IManager<Course>
    {
       
        string GenerateCourseCode(Faculty faculty);
    }
}
