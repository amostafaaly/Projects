using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models;

    public class Quiz : BaseEntity
    {
        public string CourseId { get; set; } = string.Empty;

        public string InstructorId { get; set; } = string.Empty;

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

        public DateTime ScheduledDate { get; set; }

        public string Description { get; set; } = string.Empty;

        public Quiz()
        {
            Name = $"Quiz-{Id}";
        }

        public Quiz(string courseId, string instructorId, int totalMarks, DateTime scheduledDate, string description = "")
        {
            CourseId = courseId;
            InstructorId = instructorId;
            TotalMarks = totalMarks;
            ScheduledDate = scheduledDate;
            Description = description;
            Name = $"Quiz-{CourseId}";
        }

        public string ToFileLine()
        {
            return $"{Id},{CourseId},{InstructorId},{TotalMarks},{ScheduledDate:O},{Description}";
        }

        public static Quiz FromFileLine(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 6)
                throw new FormatException("Invalid line format for Quiz.");

            var quiz = new Quiz()
            {
                Id = parts[0],
                CourseId = parts[1],
                InstructorId = parts[2],
                TotalMarks = int.Parse(parts[3]),
                ScheduledDate = DateTime.Parse(parts[4]),
                Description = parts[5]
            };

            return quiz;
        }
    }