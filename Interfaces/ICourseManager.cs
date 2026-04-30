using Projects.Models;
using Projects.IServices;

namespace Projects.Interfaces
{
    public interface ICourseManager : IManager<Course>
    {
        Course CreateCourse(Faculty faculty, string name, int creditHours, string? description = null);
        void AssignInstructor(string courseId, string instructorId);
        string GenerateCourseCode(Faculty faculty);
    }
}
