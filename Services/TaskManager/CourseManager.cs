using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Projects.Interfaces;
using Projects.Services;

namespace Projects.Services.TaskManager
{
    public class CourseManager : GenericManager<Course>, ICourseManager
    {
        private readonly IInstructorManager _instructorManager;

        public CourseManager(IInstructorManager instructorManager)
        {
            _instructorManager = instructorManager ?? throw new ArgumentNullException(nameof(instructorManager));
        }

        public Course CreateCourse(Faculty faculty, string name, int creditHours, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be empty.", nameof(name));

            var courseCode = GenerateCourseCode(faculty);
            var course = new Course
            {
                Id = courseCode,
                Name = name.Trim(),
                CreditHours = creditHours,
                Description = description?.Trim() ?? string.Empty,
                Faculty = faculty
            };

            Add(course);
            return course;
        }

        public void AssignInstructor(string courseId, string instructorId)
        {
            if (string.IsNullOrWhiteSpace(instructorId))
                throw new ArgumentException("Instructor ID cannot be empty.", nameof(instructorId));

            var course = GetById(courseId);
            var instructor = _instructorManager.GetById(instructorId);
            course.InstructorId = instructor.Id;
        }

        public string GenerateCourseCode(Faculty faculty)
        {
            var facultyName = faculty.ToString();
            var prefix = facultyName.Substring(0, 2).ToUpper();
            var code = Random.Shared.Next(100, 500);
            return $"{prefix}-{code}";
        }
    }
}
