using Projects.Src.DTOs.InstructorDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using static Projects.Src.Shared.Enums;


namespace Projects.Src.Services;

public sealed class InstructorService : IInstructorService
{
    private readonly IRepository<Instructor> _repository;
    private readonly IIdGenerator<Instructor> _idGenerator;
    private readonly IEmailGenerator<Instructor> _emailGenerator;

    public InstructorService(IRepository<Instructor> repository, IIdGenerator<Instructor> idGenerator, IEmailGenerator<Instructor> emailGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
        _emailGenerator = emailGenerator;
    }

    public void AddFulltimeInstructor(CreateFulltimeInstructorDto dto)
    {
        if (dto.MonthlySalary < 0)
            throw new SchoolException("Monthly salary cannot be negative.");
        if (!Enum.IsDefined(dto.Faculty))
            throw new SchoolException("Invalid Faculty selection.");

        var id = _idGenerator.GenerateId(dto.HiringYear);
        var email = _emailGenerator.GenerateEmail(dto.FirstName, dto.LastName);
        var instructor = new FulltimeInstructor(
            id, $"{dto.FirstName} {dto.LastName}",
            email, dto.Faculty, dto.HiringYear, dto.MonthlySalary);
        _repository.Add(instructor);
        PrintCredentials(id, email);
    }

    public void AddParttimeInstructor(CreateParttimeInstructorDto dto)
    {
        if (dto.HourlyRate < 0 || dto.HoursWorked < 0)
            throw new SchoolException("Hourly rate and hours worked cannot be negative.");
        if (!Enum.IsDefined(dto.Faculty))
            throw new SchoolException("Invalid Faculty selection.");

        var id = _idGenerator.GenerateId(dto.HiringYear);
        var email = _emailGenerator.GenerateEmail(dto.FirstName, dto.LastName);
        var instructor = new ParttimeInstructor(
            id, $"{dto.FirstName} {dto.LastName}",
            email, dto.Faculty, dto.HiringYear, dto.HourlyRate, dto.HoursWorked);
        _repository.Add(instructor);
        PrintCredentials(id, email);
    }

    private UpdateInstructorDto MapToDto(Instructor i)
    {
        if (i is FulltimeInstructor ft)
            return new FulltimeInstructorUpdateDto
            {
                Id = ft.Id,
                Name = ft.Name,
                Email = ft.UniversityEmail,
                Faculty = ft.Faculty,
                MonthlySalary = ft.MonthlySalary
            };
        else
        {
            var pt = (ParttimeInstructor)i;
            return new ParttimeInstructorUpdateDto
            {
                Id = pt.Id,
                Name = pt.Name,
                Email = pt.UniversityEmail,
                Faculty = pt.Faculty,
                HourlyRate = pt.HourlyRate,
                HoursWorked = pt.HoursWorked
            };
        }
    }
    public IEnumerable<UpdateInstructorDto> GetAllInstructors() => _repository.GetAll().Select(MapToDto);
    public UpdateInstructorDto? GetInstructorById(string id)
    {
        var instructor = _repository.GetById(id);
        return instructor == null ? null : MapToDto(instructor);
    }
    public void UpdateInstructor(UpdateInstructorDto dto)
    {
        var instructor = _repository.GetById(dto.Id)
            ?? throw new EntityNotFoundException(nameof(Instructor), dto.Id);
        if (!Enum.IsDefined(dto.Faculty))
            throw new SchoolException("Invalid Faculty selection.");

        instructor.Name = dto.Name;
        //regenerate new Email
        var nameParts = dto.Name.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        string firstName = nameParts.Length > 0 ? nameParts[0] : "instructor";
        string lastName = nameParts.Length > 1 ? nameParts[1] : "";
        instructor.UniversityEmail = _emailGenerator.GenerateEmail(firstName, lastName);
        instructor.Faculty = dto.Faculty;

        if (instructor is FulltimeInstructor ft && dto is FulltimeInstructorUpdateDto ftDto)
        {
            //update salary
            if (ftDto.MonthlySalary < 0) throw new SchoolException("Salary cannot be negative.");
            ft.MonthlySalary = ftDto.MonthlySalary;
        }
        else if (instructor is ParttimeInstructor pt && dto is ParttimeInstructorUpdateDto ptDto)
        {
            //update rates
            if (ptDto.HourlyRate < 0 || ptDto.HoursWorked < 0) throw new SchoolException("Rates/Hours cannot be negative.");
            pt.HourlyRate = ptDto.HourlyRate;
            pt.HoursWorked = ptDto.HoursWorked;
        }

        _repository.Update(instructor);
    }

    public void DeleteInstructor(string id)
    {
        var instructor = _repository.GetById(id)
            ?? throw new EntityNotFoundException(nameof(Instructor), id);
        _repository.Remove(instructor);
    }

    public IEnumerable<UpdateInstructorDto> SearchByName(string keyword)
        => _repository.Find(i => i.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)).Select(MapToDto);
    private void PrintCredentials(string id, string email)
    {
        System.Console.WriteLine($"\n  Generated ID    : {id}");
        System.Console.WriteLine($"  Generated Email : {email}");
    }
}