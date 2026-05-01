using Projects.Src.Models;

namespace Projects.Src.Contracts
{
    public interface IExamManager : IManager<Exam>
    {
        void ScheduleExam(string courseId, string instructorId, Exam exam);
        void AssignStudentToExam(string examId, string studentId);
    }
}
