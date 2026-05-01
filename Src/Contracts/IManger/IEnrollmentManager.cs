using System.Collections.Generic;
using Projects.Src.Models;

namespace Projects.Src.Contracts.IManger
{
    public interface IEnrollmentManager
    {
        void EnrollStudent(string courseId, string studentId);
        void DropStudent(string courseId, string studentId);
        List<Course> GetStudentCourses(string studentId);
        
    }
}
