using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
 public class BaseEntity
    {
        private int _id;
        private string _name;
        public int Id
        {
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Id must be greater than zero.");
                _id = value;
            }
            get { return _id; }
        }
        public string Name
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be null or empty.");
                _name = value;
            }
            get { return _name; }
        }
    }
}
