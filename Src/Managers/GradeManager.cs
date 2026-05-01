using Projects.Src.Contracts;
using Projects.Src.Models;
using Projects.Src.Services;
using Projects.Src.Utilities.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projects.Src.Managers
{
    public class GradeManager : IGradeManager
    {
        private readonly ICourseManager _courseManager;
        private readonly IGradingService _gradingService;

        public GradeManager(ICourseManager courseManager, IGradingService gradingService)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _gradingService = gradingService ?? throw new ArgumentNullException(nameof(gradingService));
        }

        public void AssignGrade(string courseId, string studentId, double rawScore)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            if (rawScore < 0 || rawScore > 100)
                throw new ArgumentOutOfRangeException(nameof(rawScore), "Score must be between 0 and 100.");

            var course = _courseManager.GetById(courseId);
            if (course == null)
                throw new KeyNotFoundException($"Course with ID '{courseId}' not found.");

            var enrollment = course.Enrollments.FirstOrDefault(e => e.StudentId == studentId);

            if (enrollment == null)
                throw new InvalidOperationException("Student is not enrolled in this course.");

            enrollment.RawScore = rawScore;
        }

        public double CalculateStudentGPA(string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var courses = _courseManager.GetAll()
                .Where(c => c.Enrollments.Any(e => e.StudentId == studentId))
                .ToList();

            double totalPoints = 0.0;
            int totalCredits = 0;

            foreach (var course in courses)
            {
                var enrollment = course.Enrollments.First(e => e.StudentId == studentId);
                if (enrollment.RawScore.HasValue)
                {
                    var points = _gradingService.GetCoursePoints((int)enrollment.RawScore.Value, 100);
                    totalPoints += points * course.CreditHours;
                    totalCredits += course.CreditHours;
                }
            }

            if (totalCredits == 0) return 0.0;

            return Math.Round(totalPoints / totalCredits, 2);
        }
    }
}
