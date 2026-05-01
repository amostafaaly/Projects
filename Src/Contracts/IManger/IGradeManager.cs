using System.Collections.Generic;
using Projects.Src.Models;

namespace Projects.Src.Contracts.IManger
{
    public interface IGradeManager
    {
        void AssignGrade(string courseId, string studentId, double rawScore);
        double CalculateStudentGPA(string studentId);
    }
}
