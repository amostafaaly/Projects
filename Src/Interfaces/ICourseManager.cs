using Projects.Src.Models.Common;
using Projects.Src.Models.Courses;

namespace Projects.Src.Interfaces
{
    public interface ICourseManager : IManager<Course>
    {
        Course CreateCourse(Faculty faculty, string name, int creditHours, string? description = null);
        void AssignInstructor(string courseId, string instructorId);
        string GenerateCourseCode(Faculty faculty);
    }
}
