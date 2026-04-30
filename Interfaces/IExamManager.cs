using Projects.Models;
using Projects.IServices;

namespace Projects.Interfaces
{
    public interface IExamManager : IManager<Exam>
    {
        void ScheduleExam(string courseId, string instructorId, Exam exam);
        void AssignStudentToExam(string examId, string studentId);
    }
}
