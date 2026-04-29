using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
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

        public int CourseId { get; set; }
       public string StudentId { get; set; }
       
       
    }
}
