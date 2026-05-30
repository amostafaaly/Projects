using Projects.Src.DTOs.StudentDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using Projects.Src.Utilities;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Services;

public sealed class StudentService : IStudentService
{
    private readonly IRepository<Student> _repository;
    private readonly IIdGenerator<Student> _idGenerator;
    private readonly IEmailGenerator<Student> _emailGenerator;

    public StudentService(IRepository<Student> repository, IIdGenerator<Student> idGenerator, IEmailGenerator<Student> emailGenerator)
    {
        _repository = repository;
        _idGenerator = idGenerator;
        _emailGenerator = emailGenerator;
    }

    public void AddStudent(CreateStudentDto dto)
    {
        if (!Enum.IsDefined(dto.Faculty))
            throw new SchoolException("Invalid Faculty selection.");

        var id = _idGenerator.GenerateId(dto.EnrollmentYear);
        var email = _emailGenerator.GenerateEmail(dto.FirstName, dto.LastName);
        var student = new Student(dto.FirstName, dto.LastName, dto.Faculty, dto.EnrollmentYear, id, email);
        _repository.Add(student);

        System.Console.WriteLine($"\n  Generated ID    : {id}");
        System.Console.WriteLine($"  Generated Email : {email}");
    }

    public IEnumerable<UpdateStudentDto> GetAllStudents()
    {
        return _repository.GetAll().Select(s => new UpdateStudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.UniversityEmail,
            Faculty = s.Faculty,
            Status = s.Status,
            Level = s.Level
        });
    }

    public UpdateStudentDto? GetStudentById(string id)
    {
        var s = _repository.GetById(id);
        if (s == null) return null;

        return new UpdateStudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.UniversityEmail,
            Faculty = s.Faculty,
            Status = s.Status,
            Level = s.Level
        };
    }
    public void UpdateStudent(UpdateStudentDto dto)
    {
        var student = _repository.GetById(dto.Id)
            ?? throw new EntityNotFoundException(nameof(Student), dto.Id);
        if (!Enum.IsDefined(dto.Faculty)) throw new SchoolException("Invalid Faculty selection.");
        if (!Enum.IsDefined(dto.Status)) throw new SchoolException("Invalid Student Status selection.");
        if (!Enum.IsDefined(dto.Level)) throw new SchoolException("Invalid Student Level selection.");

        student.Name = dto.Name;
        //regenerate new Email
        var nameParts = dto.Name.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        string firstName = nameParts.Length > 0 ? nameParts[0] : "student";
        string lastName = nameParts.Length > 1 ? nameParts[1] : "";

        student.UniversityEmail = _emailGenerator.GenerateEmail(firstName, lastName);
        student.Faculty = dto.Faculty;
        student.Status = dto.Status;
        student.Level = dto.Level;
        _repository.Update(student);
    }

    public void DeleteStudent(string id)
    {
        var student = _repository.GetById(id)
            ?? throw new EntityNotFoundException(nameof(Student), id);
        _repository.Remove(student);
    }

    public IEnumerable<UpdateStudentDto> SearchStudentsByFaculty(Faculty faculty)
    {
        return _repository.Find(s => s.Faculty == faculty && s.Status == StudentStatus.Active)
            .Select(s => new UpdateStudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.UniversityEmail,
                Faculty = s.Faculty,
                Status = s.Status,
                Level = s.Level
            });
    }
}