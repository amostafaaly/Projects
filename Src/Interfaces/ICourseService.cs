using static Projects.Src.Shared.Enums;
using Projects.Src.Models;
using Projects.Src.DTOs.CourseDTOs;

namespace Projects.Src.Interfaces;

public interface ICourseService
{
    void CreateCourse(CreateCourseDto dto);    
    void UpdateCourse(UpdateCourseDto dto);
    void DeleteCourse(string id);
    UpdateCourseDto? GetCourseById(string id);
    void AssignInstructor(string courseId, string instructorId);
    void AssignStudent(string studentId, string courseId);
    void UpdateCourseRawScore(string studentId, string courseId, double score);
    IEnumerable<EnrollmentDto> GetCoursesForStudent(string studentId);
    IEnumerable<UpdateCourseDto> GetAllCourses();
    IEnumerable<UpdateCourseDto> GetCoursesByFaculty(Faculty faculty);
}
