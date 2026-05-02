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
       
    public string GetDetails() => $"id: {Id} Name:{Name}";

    }