using Projects.Src.Models;
using Projects.Src.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Projects.Src.Contracts.Iservice;
using Projects.Src.Contracts.IManger;

namespace Projects.Src.Services
{
    public class ExamService : IExamService
    {
        private readonly IManager<Instructor> _taskManager;
        private readonly IManager<Exam> _examManager;
        private readonly IStudentManager _studentManager;
        private readonly ICourseManager? _courseManager;
        private readonly IEnrollmentService? _enrollmentService;

      

        public ExamService(
            IManager<Instructor> taskManager,
            IManager<Exam> examManager,
            IStudentManager studentManager,
            ICourseManager courseManager,
            IEnrollmentService enrollmentservice)
        {
            _taskManager = taskManager;
            _examManager = examManager;
            _studentManager = studentManager;
            _courseManager = courseManager ?? throw new ArgumentNullException(nameof(courseManager));
            _enrollmentService = enrollmentservice ?? throw new ArgumentNullException(nameof(enrollmentservice));
        }

        public void MakeExam(Instructor instructor, Exam exam)
        {
            if (instructor == null)
                throw new ArgumentNullException(nameof(instructor));

            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            var myinstructor = _taskManager.GetById(instructor.Id);

            if (_courseManager != null)
            {
                try
                {
                    var course = _courseManager.GetById(exam.CourseId);
                    if (course == null)
                        throw new KeyNotFoundException($"Course with ID '{exam.CourseId}' not found.");
                }
                catch (KeyNotFoundException ex)
                {
                    throw new InvalidOperationException($"Cannot create exam: {ex.Message}", ex);
                }
            }

            myinstructor.Exams.Add(exam);
            _examManager.Add(exam);

            Console.WriteLine(
                $"{instructor.Name} created exam for course {exam.CourseId}");
        }

        public void AssignToExam(Exam exam, Student student)
        {
            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            if (student == null)
                throw new ArgumentNullException(nameof(student));

            var mystudent = _studentManager.GetStudent(student.Id);

            if (_enrollmentService != null)
            {
                try
                {
                    var studentCourses = _enrollmentService.GetStudentCourses(student.Id);
                    var isEnrolled = studentCourses.Any(c => c.Id == exam.CourseId);
                    
                    if (!isEnrolled)
                        throw new InvalidOperationException($"Student '{student.Id}' is not enrolled in course '{exam.CourseId}'.");
                }
                catch (Exception ex) when (!(ex is InvalidOperationException))
                {
                    throw new InvalidOperationException($"Cannot assign student to exam: {ex.Message}", ex);
                }
            }

            mystudent.Exams.Add(exam);

            Console.WriteLine(
                $"Assigned {student.Name} to exam for course {exam.CourseId}");
        }
    }
}