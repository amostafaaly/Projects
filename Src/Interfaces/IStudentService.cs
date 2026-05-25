using static Projects.Src.Shared.Enums;
using Projects.Src.Models;
using Projects.Src.DTOs.StudentDTOs;

namespace Projects.Src.Interfaces;

public interface IStudentService
{
    void AddStudent(CreateStudentDto dto);
    IEnumerable<UpdateStudentDto> GetAllStudents();
    UpdateStudentDto? GetStudentById(string id);
    void UpdateStudent(UpdateStudentDto dto);
    void DeleteStudent(string id);
    IEnumerable<UpdateStudentDto> SearchStudentsByFaculty(Faculty faculty);
}
