using Projects.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    internal class FulltimeInstructor:Instructor
    {
        public decimal MonthlySalary { get; set; }

        public FulltimeInstructor   (string firstName, string lastName, Faculty faculty, int hiringYear, IInstructorIdGenerator idGenerator, IInstructorEmailGenerator emailGenerator, decimal salary)
            : base(firstName, lastName, faculty, hiringYear, idGenerator, emailGenerator)
        {
            MonthlySalary = salary;
        }

        public override decimal CalculateSalary()
        {
            return MonthlySalary;
        }
    }
}
