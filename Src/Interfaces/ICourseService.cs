using static Projects.Src.Shared.Enums;
using Projects.Src.DTOs;
using Projects.Src.Models;

namespace Projects.Src.Interfaces;

public interface ICourseService
{
    void CreateCourse(CreateCourseDto dto);
    void AssignInstructor(string courseId, string instructorId);
    IEnumerable<Course> GetAllCourses();
    Course? GetCourseById(string id);
    void UpdateCourse(UpdateCourseDto dto);
    void DeleteCourse(string id);
    IEnumerable<Course> GetCoursesByFaculty(Faculty faculty);
}
