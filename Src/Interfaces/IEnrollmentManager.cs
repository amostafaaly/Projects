using System.Collections.Generic;
using Projects.Src.Models.Courses;

namespace Projects.Src.Interfaces
{
    public interface IEnrollmentManager
    {
        void EnrollStudent(string courseId, string studentId);
        void DropStudent(string courseId, string studentId);
        void AssignGrade(string courseId, string studentId, double rawScore);
        List<Course> GetStudentCourses(string studentId);
        double CalculateStudentGPA(string studentId);
    }
}
