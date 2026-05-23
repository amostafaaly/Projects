using Projects.Src.DTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using Projects.Src.Utilities;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Services;

public sealed class CourseService : ICourseService
{
    private readonly IRepository<Course> _repository;
    private readonly CourseIdGenerator _idGenerator = new();


    public CourseService(IRepository<Course> repository)
    {
        _repository = repository;
    }

    public void CreateCourse(CreateCourseDto dto)
    {
        var course = new Course
        {
            Id = _idGenerator.GenerateId(),
            Name = dto.Name,
            CreditHours = dto.CreditHours,
            Description = dto.Description,
            Faculty = dto.Faculty
        };
        _repository.Add(course);
    }

    public void AssignInstructor(string courseId, string instructorId)
    {
        var course = _repository.GetById(courseId)
            ?? throw new EntityNotFoundException(nameof(Course), courseId);
        course.InstructorId = instructorId;
        _repository.Update(course);
    }

    public IEnumerable<Course> GetAllCourses()
        => _repository.GetAll();

    public Course? GetCourseById(string id)
        => _repository.GetById(id);

    public void UpdateCourse(UpdateCourseDto dto)
    {
        var course = _repository.GetById(dto.Id)
            ?? throw new EntityNotFoundException(nameof(Course), dto.Id);
        course.Name = dto.Name;
        course.CreditHours = dto.CreditHours;
        course.Description = dto.Description;
        course.Faculty = dto.Faculty;
        _repository.Update(course);
    }

    public void DeleteCourse(string id)
    {
        var course = _repository.GetById(id)
            ?? throw new EntityNotFoundException(nameof(Course), id);
        _repository.Remove(course);
    }

    public IEnumerable<Course> GetCoursesByFaculty(Faculty faculty)
        => _repository.Find(c => c.Faculty == faculty)
                      .OrderBy(c => c.Name);
}