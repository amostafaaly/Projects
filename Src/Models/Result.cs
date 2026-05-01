using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models;

    public class Result : BaseEntity
    {
        public Result()
        {
            Name = $"Result-{Id}";
        }

        public string StudentId { get; set; } = string.Empty;

        public string ExamId { get; set; } = string.Empty;

        public int Score { get; set; }

        private int _totalMarks;
        public int TotalMarks
        {
            get => _totalMarks;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(TotalMarks), "Total marks must be greater than zero.");
                _totalMarks = value;
            }
        }


        // Grade calculation moved to GradingService to keep model focused on data only.
        public string ToFileLine()
        {
            return $"{Id},{StudentId},{ExamId},{Score},{TotalMarks}";
        }
        public static Result FromFileLine(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 5)
                throw new FormatException("Invalid line format for Result.");
            var result = new Result();
            result.Id = parts[0];
            result.StudentId = parts[1];
            result.ExamId = parts[2];
            result.Score = int.Parse(parts[3]);
            result.TotalMarks = int.Parse(parts[4]);
            return result;
        }
    }