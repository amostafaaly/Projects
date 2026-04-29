using Projects.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public class ParttimeInstructorcs:Instructor
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public ParttimeInstructorcs(string firstName, string lastName, Faculty faculty, int hiringYear, IInstructorIdGenerator idGenerator, IInstructorEmailGenerator emailGenerator, decimal hourlyRate, int hoursWorked)
            : base(firstName, lastName, faculty, hiringYear, idGenerator, emailGenerator)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        }
        public override decimal CalculateSalary()
        {
            return HourlyRate * HoursWorked;
        }
    
    }
}
