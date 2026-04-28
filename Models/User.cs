using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public abstract class User
    {
        private int Id { get; set; }
        private string Name { get; set; }
        public int id
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Id must be greater than zero.");
                Id = value;
            }
            get { return Id; }
        }
        public string name
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                Name = value;
            }
            get { return Name; }
        }
        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public abstract void DisplayInfo();
       
    }
}
