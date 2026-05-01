using Projects.Src.Contracts.IManger;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projects.Src.Managers
{
    public class EnrollmentManager : IEnrollmentManager
    {
        private readonly ICourseManager _courseManager;
        private readonly IStudentManager _studentManager;

        public EnrollmentManager(ICourseManager courseManager, IStudentManager studentManager)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _studentManager = studentManager ?? throw new ArgumentNullException(nameof(studentManager));
        }

        public void EnrollStudent(string courseId, string studentId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var course = _courseManager.GetById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course with ID '{courseId}' not found.");

            if (string.IsNullOrWhiteSpace(course.InstructorId))
                throw new InvalidOperationException("Instructor must be assigned before enrollment.");

            var student = _studentManager.GetStudent(studentId);
            if (student == null)
                throw new KeyNotFoundException($"Student with ID '{studentId}' not found.");

            if (course.Enrollments.Any(e => e.StudentId == studentId))
                throw new InvalidOperationException("Student is already enrolled in this course.");

            course.Enrollments.Add(new StudentCourse { StudentId = studentId, CourseId = courseId });
        }

        public void DropStudent(string courseId, string studentId)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var course = _courseManager.GetById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course with ID '{courseId}' not found.");

            var enrollment = course.Enrollments.FirstOrDefault(e => e.StudentId == studentId);

            if (enrollment == null)
                throw new InvalidOperationException("Student is not enrolled in this course.");

            course.Enrollments.Remove(enrollment);
        }


        public List<Course> GetStudentCourses(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            return _courseManager.GetAll()
                .Where(course => course.Enrollments.Any(e => e.StudentId == studentId))
                .ToList();
        }

        
    }
}
