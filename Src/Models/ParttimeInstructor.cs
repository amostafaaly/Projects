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

        public string ToFileLine()
        {
            return $"{Id},{Name},{UniversityEmail},{HiringYear},{Faculty},{Status},{HourlyRate},{HoursWorked}";
        }

        public static ParttimeInstructor FromFileLine(string line)
        {
            var parts = line.Split(',');

            if (parts.Length != 8)
                throw new FormatException("Invalid line format for ParttimeInstructor.");

            var faculty = Enum.Parse<Faculty>(parts[4]);
            var status = Enum.Parse<InstructorStatus>(parts[5]);

            var instructor = new ParttimeInstructor(
                parts[0],
                parts[1],
                parts[2],
                faculty,
                int.Parse(parts[3]),
                decimal.Parse(parts[6]),
                int.Parse(parts[7])
            );

            instructor.ChangeStatus(status);

            return instructor;
        }
    }