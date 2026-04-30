using System;

namespace Projects.Models
{
    public class StudentCourse
    {
        public string StudentId { get; set; } = string.Empty;
        public string CourseId { get; set; } = string.Empty;

        private double? _rawScore;
        
        public double? RawScore
        {
            get => _rawScore;
            set
            {
                if (value.HasValue && (value.Value < 0 || value.Value > 100))
                    throw new ArgumentOutOfRangeException(nameof(RawScore), "Score must be between 0 and 100.");
                _rawScore = value;
            }
        }

        public double CoursePoints
        {
            get
            {
                if (!RawScore.HasValue) return 0.0;
                if (RawScore >= 90) return 4.0;
                if (RawScore >= 80) return 3.0;
                if (RawScore >= 70) return 2.0;
                if (RawScore >= 60) return 1.0;
                return 0.0;
            }
        }

        public string LetterGrade
        {
            get
            {
                if (!RawScore.HasValue) return "N/A";
                if (RawScore >= 90) return "A";
                if (RawScore >= 80) return "B";
                if (RawScore >= 70) return "C";
                if (RawScore >= 60) return "D";
                return "F";
            }
        }
    }
}
