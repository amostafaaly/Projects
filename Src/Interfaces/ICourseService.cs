using static Projects.Src.Shared.Enums;
using Projects.Src.Models;
using Projects.Src.DTOs.CourseDTOs;

namespace Projects.Src.Interfaces;

public interface ICourseService
{
    void CreateCourse(CreateCourseDto dto);    
    void UpdateCourse(UpdateCourseDto dto);
    void DeleteCourse(string id);
    Course? GetCourseById(string id);
    void AssignInstructor(string courseId, string instructorId);
    void AssignStudent(string studentId, string courseId);
    void UpdateCourseRawScore(string studentId, string courseId, double score);
    IEnumerable<EnrollmentDto> GetCoursesForStudent(string studentId);
    IEnumerable<Course> GetAllCourses();
    IEnumerable<Course> GetCoursesByFaculty(Faculty faculty);
}
