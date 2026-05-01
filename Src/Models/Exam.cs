using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models;

    public class Exam: BaseEntity
    {
       
      
        public DateTime Date { get; set; }
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
        public Exam(DateTime date,  int totalMarks  )
        {
            Date = date;
            TotalMarks = totalMarks;
           
        }

        public string CourseId { get; set; }
       
       public string InstructorId { get; set; }
        public string ToFileLine()
        {
            return $"{Id},{Date:O},{TotalMarks}";
        }
        public void FromFileLine(string line)
        {
            var parts = line.Split(',');
            if (parts.Length != 6)
                throw new FormatException("Invalid line format for Exam.");
            Id = parts[0];
            Date = DateTime.Parse(parts[1]);
            TotalMarks = int.Parse(parts[2]);
            CourseId = parts[3];
         
            InstructorId = parts[5];
        }
    public string GetDetails() => $"id: {Id} Name:{Name}";

    }