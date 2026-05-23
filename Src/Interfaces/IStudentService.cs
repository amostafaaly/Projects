using static Projects.Src.Shared.Enums;
using Projects.Src.Models;
using Projects.Src.DTOs.StudentDTOs;

namespace Projects.Src.Interfaces;

public interface IStudentService
{
    void AddStudent(CreateStudentDto dto);
    IEnumerable<Student> GetAllStudents();
    Student? GetStudentById(string id);
    void UpdateStudent(UpdateStudentDto dto);
    void DeleteStudent(string id);
    IEnumerable<Student> SearchStudentsByFaculty(Faculty faculty);
}
