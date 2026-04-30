using System.Collections.Generic;
using Projects.Models;

namespace Projects.Interfaces
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
