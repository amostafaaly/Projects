using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
   public abstract class Instructor : User
    {
        public InstructorStatus Status { get; set; }
        public string Faculty { get; set; }
      

       
        public Instructor(int id, string name, InstructorStatus status, string faculty) : base(id, name)
        {
            Status = status;
            Faculty = faculty;
           

        }
        public abstract decimal CalculateSalary();

        public override void DisplayInfo()
        {
            Console.WriteLine($"Instructor ID: {Id}, Name: {Name}, Status: {Status}, Faculty: {Faculty}");
        }
    }
}
