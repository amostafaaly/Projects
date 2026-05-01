using System;

namespace Projects.Src.Services
{
    public class GradingService : IGradingService
    {
        public string GetLetterGrade(int score, int totalMarks)
        {
            if (totalMarks <= 0) throw new ArgumentOutOfRangeException(nameof(totalMarks));
            double percentage = (double)score / totalMarks * 100;

            if (percentage >= 90) return "A";
            if (percentage >= 80) return "B";
            if (percentage >= 70) return "C";
            if (percentage >= 60) return "D";
            return "F";
        }

        public double GetCoursePoints(int score, int totalMarks)
        {
            var letter = GetLetterGrade(score, totalMarks);
            return letter switch
            {
                "A" => 4.0,
                "B" => 3.0,
                "C" => 2.0,
                "D" => 1.0,
                _ => 0.0,
            };
        }
    }
}
