using Projects.Src.Models;

namespace Projects.Src.Contracts.IManger
{
    public interface ICourseManager : IManager<Course>
    {
        Course CreateCourse(Faculty faculty, string name, int creditHours, string? description = null);
        void AssignInstructor(string courseId, string instructorId);
        string GenerateCourseCode(Faculty faculty);
    }
}
