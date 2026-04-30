using System.Collections.Generic;
using Projects.Models;

namespace Projects.Interfaces
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
