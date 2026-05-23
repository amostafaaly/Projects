using static Projects.Src.Shared.Enums;
using Projects.Src.DTOs;
using Projects.Src.Models;

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
