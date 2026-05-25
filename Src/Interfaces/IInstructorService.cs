using Projects.Src.Models;
using Projects.Src.DTOs.InstructorDTOs;

namespace Projects.Src.Interfaces;

public interface IInstructorService
{
    void AddFulltimeInstructor(CreateFulltimeInstructorDto dto);
    void AddParttimeInstructor(CreateParttimeInstructorDto dto);
    IEnumerable<UpdateInstructorDto> GetAllInstructors();
    UpdateInstructorDto? GetInstructorById(string id);
    void UpdateInstructor(UpdateInstructorDto dto);
    void DeleteInstructor(string id);
    IEnumerable<UpdateInstructorDto> SearchByName(string keyword);
}
