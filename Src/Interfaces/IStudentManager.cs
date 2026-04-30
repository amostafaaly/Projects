using System.Collections.Generic;
using Projects.Src.Models.Common;
using Projects.Src.Models.Students;

namespace Projects.Src.Interfaces
{
    public interface IStudentManager
    {
        IEnumerable<Student> AllStudents { get; }
        Student RegisterStudent(string firstName, string lastName, int enrollmentYear, Faculty faculty);
        void PromoteToGraduate(string studentId);
        void ChangeFaculty(string studentId, Faculty newFaculty);
        void ChangeStatus(string studentId, StudentStatus newStatus);
        void DeleteStudent(string studentId);
        Student GetStudent(string id);
    }
}
