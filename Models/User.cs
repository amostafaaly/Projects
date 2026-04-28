using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public abstract class User:BaseEntity
    {

            
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void DisplayInfo();
       
    }
}
