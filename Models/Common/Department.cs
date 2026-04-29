using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public  class Department:BaseEntity
    {
       
       public Department(string id, string name)
        {
            Id = id;
            Name = name;
          StudentIds = new List<int>();
           InstructorIds = new List<int>();

        }

        public List<int> StudentIds { get; private set; } 
        public List<int> InstructorIds { get; private set; } 
    }
}
