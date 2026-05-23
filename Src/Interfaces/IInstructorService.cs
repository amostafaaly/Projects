using static Projects.Src.Shared.Enums;
using Projects.Src.DTOs;
using Projects.Src.Models;

namespace Projects.Src.Interfaces;

public interface IInstructorService
{
    void AddFulltimeInstructor(CreateFulltimeInstructorDto dto);
    void AddParttimeInstructor(CreateParttimeInstructorDto dto);
    IEnumerable<Instructor> GetAllInstructors();
    Instructor? GetInstructorById(string id);
    void UpdateInstructor(UpdateInstructorDto dto);
    void DeleteInstructor(string id);
    IEnumerable<Instructor> SearchByName(string keyword);
}
