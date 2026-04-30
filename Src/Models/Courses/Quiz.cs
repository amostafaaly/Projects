using Projects.Src.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models.Courses
{
    public class Quiz:BaseEntity
    {
       

        public DateTime Date { get; set; }
        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Score cannot be negative.");
                _score = value;
            }
        }
       
        public int CourseId { get; set; }
    }
}
