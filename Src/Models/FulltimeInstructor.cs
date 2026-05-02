using Projects.Src.Contracts;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Projects.Src.Models;

    public class FulltimeInstructor:Instructor
    {
        

        public decimal MonthlySalary { get; set; }

        public FulltimeInstructor(
            string id,
            string name,
            string universityEmail,
            Faculty faculty,
            int hiringYear,
            decimal salary)
            : base(id, name, universityEmail, faculty, hiringYear)
        {
            MonthlySalary = salary;
        Status = InstructorStatus.FullTime;
    }

     

        public override decimal CalculateSalary()
        {
            return MonthlySalary;
        }
      

    }