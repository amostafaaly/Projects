using System.Collections.Generic;
using Projects.Src.Models;

namespace Projects.Src.Contracts.IManger
{
    public interface IStudentManager
    {
        IEnumerable<Student> AllStudents { get; }
        void AddStudent(Student student);
        Student RegisterStudent(string firstName, string lastName, int enrollmentYear, Faculty faculty);
        void PromoteToGraduate(string studentId);
        void ChangeFaculty(string studentId, Faculty newFaculty);
        void ChangeStatus(string studentId, StudentStatus newStatus);
        void DeleteStudent(string studentId);
        Student GetStudent(string id);
    }
}
