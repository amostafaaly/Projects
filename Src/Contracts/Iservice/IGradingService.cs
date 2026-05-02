using System;

namespace Projects.Src.Contracts.Iservice
{
    public interface IGradingService
    {
        string GetLetterGrade(int score, int totalMarks);
        double GetCoursePoints(int score, int totalMarks);
        public void AssignGrade(string courseId, string studentId, double rawScore);
        public double CalculateStudentGPA(string studentId);
    }
}
