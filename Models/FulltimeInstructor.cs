using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    internal class FulltimeInstructor:Instructor
    {
        public decimal MonthlySalary { get; set; }

        public FulltimeInstructor   (int id, string name, FulltimeInstructor status,string faculty,decimal salary)
            : base(id, name,InstructorStatus.FullTime,faculty)
        {
            MonthlySalary = salary;
        }

        public override decimal CalculateSalary()
        {
            return MonthlySalary;
        }
    }
}
