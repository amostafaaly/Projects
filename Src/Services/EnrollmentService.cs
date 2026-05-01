using Projects.Src.Contracts;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Projects.Src.Services
{
    public class EnrollmentService
    {
        private readonly ICourseManager _courseManager;
        private readonly IStudentManager _studentManager;
        private readonly IGradeManager _gradeManager;

        public EnrollmentService(ICourseManager courseManager, IStudentManager studentManager, IGradeManager gradeManager)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _studentManager = studentManager ?? throw new ArgumentNullException(nameof(studentManager));
            _gradeManager = gradeManager ?? throw new ArgumentNullException(nameof(gradeManager));
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
            Console.WriteLine($"{student.Name} enrolled in course {course.Name}");
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
            Console.WriteLine($"Student {studentId} dropped from course {courseId}");
        }

        public void AssignGrade(string courseId, string studentId, double rawScore)
        {
            _gradeManager.AssignGrade(courseId, studentId, rawScore);
            Console.WriteLine($"Grade assigned to student {studentId}: {rawScore} in course {courseId}");
        }

        public List<Course> GetStudentCourses(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            return _courseManager.GetAll()
                .Where(course => course.Enrollments.Any(e => e.StudentId == studentId))
                .ToList();
        }

        public double CalculateStudentGPA(string studentId)
        {
            return _gradeManager.CalculateStudentGPA(studentId);
        }
    }
}
