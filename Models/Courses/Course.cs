using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public class Course: BaseEntity
    {
   

        public string Description { get; set; }
        private      int CreditHours     { get; set; }
        public int creditHours
        {
            get { return CreditHours; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Credit hours cannot be negative.");
                CreditHours = value;
            }
        }
       

        public int InstructorId { get; set; }
        public List<int> StudentIds { get; set; } = new List<int>();
       

    }
}
