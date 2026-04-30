using Projects.Interfaces;
using Projects.Models;
using System;

namespace Projects.Services.TaskManager
{
    public class ExamManager : GenericManager<Exam>, IExamManager
    {
        private readonly ICourseManager _courseManager;
        private readonly IStudentManager _studentManager;

        public ExamManager(ICourseManager courseManager, IStudentManager studentManager)
        {
            _courseManager = courseManager;
            _studentManager = studentManager;
        }

        public void ScheduleExam(string courseId, string instructorId, Exam exam)
        {
            var course = _courseManager.GetById(courseId);
            exam.CourseId = int.Parse(courseId);
            Add(exam);
        }

        public void AssignStudentToExam(string examId, string studentId)
        {
            var exam = GetById(examId);
            exam.StudentId = studentId;
        }
    }
}
