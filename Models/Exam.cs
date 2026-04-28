using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public class Exam: BaseEntity
    {
       
      
        public DateTime Date { get; set; }
        private int _totalMarks { get; set; }
      
        public int CourseId { get; set; }
       
        public  int TotalMarks { get { return _totalMarks; }
            set
            {
                if (value<0)
                {
                    throw new IndexOutOfRangeException("value must be larger than zero");
                }
                _totalMarks = value;
            }
        }
       
    }
}
