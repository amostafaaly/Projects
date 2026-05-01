using Projects.Src.Contracts;
using Projects.Src.Models;
using System;

namespace Projects.Src.Managers
{
    public class ExamManager : GenericManager<Exam>, IExamManager
    {
        private readonly ICourseManager? _courseManager;
        private readonly IEnrollmentManager? _enrollmentManager;

        public ExamManager()
        {
            _courseManager = null;
            _enrollmentManager = null;
        }

        public ExamManager(ICourseManager courseManager, IEnrollmentManager enrollmentManager)
        {
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _enrollmentManager = enrollmentManager ?? throw new ArgumentNullException(nameof(enrollmentManager));
        }

        public void ScheduleExam(string courseId, string instructorId, Exam exam)
        {
            if (string.IsNullOrWhiteSpace(courseId))
                throw new ArgumentException("Course ID cannot be empty.", nameof(courseId));

            if (string.IsNullOrWhiteSpace(instructorId))
                throw new ArgumentException("Instructor ID cannot be empty.", nameof(instructorId));

            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            if (_courseManager != null)
            {
                var course = _courseManager.GetById(courseId);
                if (course == null)
                    throw new KeyNotFoundException($"Course with ID '{courseId}' not found.");
            }

            exam.CourseId = courseId;
            exam.InstructorId = instructorId;

            if (string.IsNullOrWhiteSpace(exam.Id))
                exam.Id = Guid.NewGuid().ToString();

            Add(exam);
        }

        public void AssignStudentToExam(string examId, string studentId)
        {
            if (string.IsNullOrWhiteSpace(examId))
                throw new ArgumentException("Exam ID cannot be empty.", nameof(examId));

            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            var exam = GetById(examId);
            
            if (_enrollmentManager != null)
            {
                var studentCourses = _enrollmentManager.GetStudentCourses(studentId);
                var isEnrolled = studentCourses.Any(c => c.Id == exam.CourseId);
                
                if (!isEnrolled)
                    throw new InvalidOperationException($"Student '{studentId}' is not enrolled in course '{exam.CourseId}'.");
            }

            exam.StudentId = studentId;
        }
    }
}
