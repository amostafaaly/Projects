using Projects.Src.DTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using Projects.Src.Utilities;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Services;

public sealed class StudentService : IStudentService
{
    private readonly IRepository<Student> _repository;
    private readonly StudentIdGenerator _idGenerator = new();
    private readonly StudentEmailGenerator _emailGenerator = new();

    public StudentService(IRepository<Student> repository)
    {
        _repository = repository;
    }

    public void AddStudent(CreateStudentDto dto)
    {
        var id = _idGenerator.GenerateId(dto.EnrollmentYear);
        var email = _emailGenerator.GenerateEmail(dto.FirstName, dto.LastName);
        var student = new Student(dto.FirstName, dto.LastName, dto.Faculty, dto.EnrollmentYear, id, email);
        _repository.Add(student);
    }

    public IEnumerable<Student> GetAllStudents()
        => _repository.GetAll();

    public Student? GetStudentById(string id)
        => _repository.GetById(id);

    public void UpdateStudent(UpdateStudentDto dto)
    {
        var student = _repository.GetById(dto.Id)
            ?? throw new EntityNotFoundException(nameof(Student), dto.Id);
        student.Name = dto.Name;
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

    public IEnumerable<Student> SearchStudentsByFaculty(Faculty faculty)
        => _repository.Find(s => s.Faculty == faculty && s.Status == StudentStatus.Active);
}