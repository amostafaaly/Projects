using static Projects.Src.Shared.Enums;
using Projects.Src.Models;
using Projects.Src.DTOs.InstructorDTOs;

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
