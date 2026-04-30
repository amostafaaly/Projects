using Projects.Src.Models.Courses;

namespace Projects.Src.Interfaces
{
    public interface IExamManager : IManager<Exam>
    {
        void ScheduleExam(string courseId, string instructorId, Exam exam);
        void AssignStudentToExam(string examId, string studentId);
    }
}
