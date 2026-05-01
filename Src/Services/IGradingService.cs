using System;

namespace Projects.Src.Services
{
    public interface IGradingService
    {
        string GetLetterGrade(int score, int totalMarks);
        double GetCoursePoints(int score, int totalMarks);
    }
}
