using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Models
{
    public class ParttimeInstructorcs:Instructor
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }
        public ParttimeInstructorcs(int id, string name, InstructorStatus status, string faculty, decimal hourlyRate, int hoursWorked)
            : base(id, name, InstructorStatus.PartTime, faculty)
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
