using Projects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Projects.Interfaces;

namespace Projects.Services.TaskManager
{
    public class EnrollmentManager : IEnrollmentManager
    {
        private readonly ICourseManager _courseManager;
        private readonly IStudentManager _studentManager;

        public EnrollmentManager(ICourseManager courseManager, IStudentManager studentManager)
        {
            _courseManager = courseManager;
            _studentManager = studentManager;
        }
        
        public void EnrollStudent(string courseId, string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var course = _courseManager.GetById(courseId);
            if (string.IsNullOrWhiteSpace(course.InstructorId))
                throw new InvalidOperationException("Instructor must be assigned before enrollment.");

            _studentManager.GetStudent(studentId);
            if (course.Enrollments.Any(e => e.StudentId == studentId))
                throw new InvalidOperationException("Student is already enrolled in this course.");

            course.Enrollments.Add(new StudentCourse { StudentId = studentId, CourseId = courseId });
        }

        public void DropStudent(string courseId, string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var course = _courseManager.GetById(courseId);
            var enrollment = course.Enrollments.FirstOrDefault(e => e.StudentId == studentId);
            
            if (enrollment == null)
                throw new InvalidOperationException("Student is not enrolled in this course.");
                
            course.Enrollments.Remove(enrollment);
        }

        public void AssignGrade(string courseId, string studentId, double rawScore)
        {
            var course = _courseManager.GetById(courseId);
            var enrollment = course.Enrollments.FirstOrDefault(e => e.StudentId == studentId);
            
            if (enrollment == null)
                throw new InvalidOperationException("Student is not enrolled in this course.");
                
            enrollment.RawScore = rawScore;
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
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var studentCourses = GetStudentCourses(studentId);
            
            double totalPoints = 0.0;
            int totalCredits = 0;

            foreach (var course in studentCourses)
            {
                var enrollment = course.Enrollments.First(e => e.StudentId == studentId);
                
                if (enrollment.RawScore.HasValue) 
                {
                    totalPoints += enrollment.CoursePoints * course.CreditHours;
                    totalCredits += course.CreditHours;
                }
            }

            if (totalCredits == 0) return 0.0;

            return Math.Round(totalPoints / totalCredits, 2); 
        }
    }
}
