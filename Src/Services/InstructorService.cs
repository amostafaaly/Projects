using Projects.Src.DTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using Projects.Src.Utilities;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Services;

public sealed class InstructorService : IInstructorService
{
    private readonly IRepository<Instructor> _repository;
    private readonly InstructorIdGenerator _idGenerator = new();
    private readonly InstructorEmailGenerator _emailGenerator = new();

    public InstructorService(IRepository<Instructor> repository)
    {
        _repository = repository;
    }

    public void AddFulltimeInstructor(CreateFulltimeInstructorDto dto)
    {
        var id = _idGenerator.GenerateId(dto.HiringYear);
        var email = _emailGenerator.GenerateEmail(dto.FirstName, dto.LastName);
        var instructor = new FulltimeInstructor(
            id, $"{dto.FirstName} {dto.LastName}",
            email, dto.Faculty, dto.HiringYear, dto.MonthlySalary);
        _repository.Add(instructor);
    }

    public void AddParttimeInstructor(CreateParttimeInstructorDto dto)
    {
        var id = _idGenerator.GenerateId(dto.HiringYear);
        var email = _emailGenerator.GenerateEmail(dto.FirstName, dto.LastName);
        var instructor = new ParttimeInstructor(
            id, $"{dto.FirstName} {dto.LastName}",
            email, dto.Faculty, dto.HiringYear, dto.HourlyRate, dto.HoursWorked);
        _repository.Add(instructor);
    }

    public IEnumerable<Instructor> GetAllInstructors()
        => _repository.GetAll();

    public Instructor? GetInstructorById(string id)
        => _repository.GetById(id);

    public void UpdateInstructor(UpdateInstructorDto dto)
    {
        var instructor = _repository.GetById(dto.Id)
            ?? throw new EntityNotFoundException(nameof(Instructor), dto.Id);
        instructor.Name = dto.Name;
        instructor.Faculty = dto.Faculty;
        _repository.Update(instructor);
    }

    public void DeleteInstructor(string id)
    {
        var instructor = _repository.GetById(id)
            ?? throw new EntityNotFoundException(nameof(Instructor), id);
        _repository.Remove(instructor);
    }

    public IEnumerable<Instructor> SearchByName(string keyword)
        => _repository.Find(i =>
            i.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
}