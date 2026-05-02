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

       
    }