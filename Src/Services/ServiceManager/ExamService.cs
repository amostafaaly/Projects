using Projects.Interfaces;
using Projects.Src.Interfaces;
using Projects.Src.Models.Courses;
using Projects.Src.Models.Instructors;
using Projects.Src.Models.Students;
using Projects.Src.Services.TaskManager;
using System.Collections.Generic;
using System.Text;

namespace Projects.Services.ServiceManager
{
    public class ExamService : IExamService
    {
        private readonly IManager<Instructor> _taskManager;
        private readonly IManager<Exam> _examManager;
        private readonly StudentManager _studentManager;

        public ExamService(
            IManager<Instructor> taskManager,
            IManager<Exam> examManager,
            StudentManager studentManager)
        {
            _taskManager = taskManager;
            _examManager = examManager;
            _studentManager = studentManager;
        }

        public void MakeExam(Instructor instructor, Exam exam)
        {
            if (instructor == null)
                throw new ArgumentNullException(nameof(instructor));

            if (exam == null)
                throw new ArgumentNullException(nameof(exam));

            var myinstructor = _taskManager.GetById(instructor.Id);

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

            mystudent.Exams.Add(exam);

            Console.WriteLine(
                $"Assigned {student.Name} to exam for course {exam.CourseId}");
        }
    }
}