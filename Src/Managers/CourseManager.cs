using System;
using System.Collections.Generic;
using System.Linq;
using Projects.Src.Contracts;
using Projects.Src.Models;

namespace Projects.Src.Managers
{
    public class CourseManager : GenericManager<Course>, ICourseManager
    {
        public string GenerateCourseCode(Faculty faculty)
        {
            var facultyName = faculty.ToString();
            var prefix = facultyName.Substring(0, 2).ToUpper();
            var code = Random.Shared.Next(100, 500);
            return $"{prefix}-{code}";
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

            Add(course);
            return course;
        }

        public void AssignInstructor(string courseId, string instructorId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(instructorId))
                throw new ArgumentException("Instructor ID cannot be empty.", nameof(instructorId));

            var course = GetById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course with ID '{courseId}' not found.");

            course.InstructorId = instructorId;
        }
    }
}


