using Projects.Src.DTOs.CourseDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using Projects.Src.Utilities;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Services;

public sealed class CourseService : ICourseService
{
    private readonly IRepository<Course> _repository;
    private readonly IRepository<StudentCourse> _studentCourseRepo;
    private readonly IRepository<Student> _studentRepo;
    private readonly CourseIdGenerator _idGenerator = new();


    public CourseService(IRepository<Course> repository, IRepository<StudentCourse> studentCourseRepo, IRepository<Student> studentRepo)
    {
        _repository = repository;
        _studentCourseRepo = studentCourseRepo;
        _studentRepo = studentRepo;
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
        Console.WriteLine($"\n  Generated Course ID: {course.Id} - Corse Name: {course.Name}");
    }

    public void AssignInstructor(string courseId, string instructorId)
    {
        var course = _repository.GetById(courseId)
            ?? throw new EntityNotFoundException(nameof(Course), courseId);
        course.InstructorId = instructorId;
        _repository.Update(course);
    }

    public void AssignStudent(string studentId, string courseId)
    {
        if (_studentRepo.GetById(studentId) == null) throw new EntityNotFoundException(nameof(Student), studentId);
        if (_repository.GetById(courseId) == null) throw new EntityNotFoundException(nameof(Course), courseId);

        if (_studentCourseRepo.Find(sc => sc.StudentId == studentId && sc.CourseId == courseId).Any())
            throw new SchoolException($"Student '{studentId}' is already enrolled in course '{courseId}'");

        _studentCourseRepo.Add(new StudentCourse { StudentId = studentId, CourseId = courseId });
    }

    public IEnumerable<EnrollmentDto> GetCoursesForStudent(string studentId)
    {
        if (_studentRepo.GetById(studentId) == null) throw new EntityNotFoundException(nameof(Student), studentId);
        var enrollments = _studentCourseRepo.Find(sc => sc.StudentId == studentId).ToList();
        if (!enrollments.Any()) throw new SchoolException($"No course enrollments found for student '{studentId}'.");

        return enrollments.Select(e => new EnrollmentDto
        {
            CourseId = e.CourseId,
            CourseName = _repository.GetById(e.CourseId)?.Name ?? "Unknown Course",
            RawScore = e.RawScore
        });
    }

    public void UpdateCourseRawScore(string studentId, string courseId, double score)
    {
        if (score < 0 || score > 100)
            throw new SchoolException("Score must be between 0 and 100.");

        var enrollment = _studentCourseRepo.Find(sc => sc.StudentId == studentId && sc.CourseId == courseId).FirstOrDefault()
            ?? throw new SchoolException($"Student '{studentId}' is not enrolled in course '{courseId}'.");

        enrollment.RawScore = score;
        _studentCourseRepo.Update(enrollment);
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