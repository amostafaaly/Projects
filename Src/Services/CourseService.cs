using Projects.Src.Contracts.IManger;
using Projects.Src.Contracts.Iservice;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Services
{
    public class CourseService:IcourseService
    {
        private readonly ICourseManager _courseManager;
        private readonly IInstructorManager _instructorManager;

        public CourseService(ICourseManager courseManager, IInstructorManager instructorManager)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _instructorManager = instructorManager ?? throw new ArgumentNullException(nameof(instructorManager));
        }

        public Course CreateCourse(Faculty faculty, string name, int creditHours, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be empty.", nameof(name));

            if (creditHours <= 0)
                throw new ArgumentException("Credit hours must be greater than zero.", nameof(creditHours));

            var courseCode = GenerateCourseCode(faculty);
            var course = new Course
            {
                Id = courseCode,
                Name = name.Trim(),
                CreditHours = creditHours,
                Description = description?.Trim() ?? string.Empty,
                Faculty = faculty
            };

            _courseManager.Add(course);
            Console.WriteLine($"Course created: {course.Name} ({course.Id})");
            return course;
        }

        public void AssignInstructor(string courseId, string instructorId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(instructorId))
                throw new ArgumentException("Instructor ID cannot be empty.", nameof(instructorId));

            var course = _courseManager.GetById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course with ID '{courseId}' not found.");

            var instructor = _instructorManager.GetById(instructorId);
            if (instructor == null)
                throw new KeyNotFoundException($"Instructor with ID '{instructorId}' not found.");

            course.InstructorId = instructor.Id;
            Console.WriteLine($"Instructor {instructor.Name} assigned to course {course.Name}");
        }

        public string GenerateCourseCode(Faculty faculty)
        {
            var facultyName = faculty.ToString();
            var prefix = facultyName.Substring(0, 2).ToUpper();
            var code = Random.Shared.Next(100, 500);
            return $"{prefix}-{code}";
        }

        public Course GetCourse(string courseId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            return _courseManager.GetById(courseId);
        }

        public List<Course> GetAllCourses()
        {
            return _courseManager.GetAll();
        }
    }
}
