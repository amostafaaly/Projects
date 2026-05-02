using Projects.Src.Contracts;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models;

    public class ParttimeInstructor : Instructor
    {
        public decimal HourlyRate { get; set; }
        public int HoursWorked { get; set; }

        public ParttimeInstructor(
            string id,
            string name,
            string universityEmail,
            Faculty faculty,
            int hiringYear,
            decimal hourlyRate,
            int hoursWorked)
            : base(id, name, universityEmail, faculty, hiringYear)
        {
            HourlyRate = hourlyRate;
            HoursWorked = hoursWorked;
        Status = InstructorStatus.PartTime;
        }

        public override decimal CalculateSalary()
        {
            return HourlyRate * HoursWorked;
        }

    
    }