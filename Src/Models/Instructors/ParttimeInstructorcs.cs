using Projects.Src.Interfaces;
using Projects.Src.Models.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Models.Instructors
{
    public class ParttimeInstructorcs : Instructor
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
        public string ToFileLine()
        {
            return $"{Id},{Name},{UniversityEmail},{HiringYear},{Faculty},{Status},{HourlyRate},{HoursWorked}";
        }
        public static ParttimeInstructorcs FromFileLine(
          string line,
          IInstructorIdGenerator idGenerator,
          IInstructorEmailGenerator emailGenerator)
        {
            var parts = line.Split(',');

            if (parts.Length != 8)
                throw new FormatException("Invalid line format for ParttimeInstructor.");

            var faculty = Enum.Parse<Faculty>(parts[4]);
            var status = Enum.Parse<InstructorStatus>(parts[5]);

            var instructor = new ParttimeInstructorcs(
                parts[1], 
                "",       
                faculty,
                int.Parse(parts[3]),
                idGenerator,
                emailGenerator,
                decimal.Parse(parts[6]),
                int.Parse(parts[7])
            );

            instructor.Id = parts[0];
            instructor.ChangeStatus(status);

            return instructor;
        }
    }
}
