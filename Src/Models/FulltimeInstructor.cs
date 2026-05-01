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
        }

     

        public override decimal CalculateSalary()
        {
            return MonthlySalary;
        }
        public string ToFileLine()
        {
            return $"{Id},{Name},{UniversityEmail},{HiringYear},{Faculty},{Status},{MonthlySalary}";
        }
        public static FulltimeInstructor FromFileLine(string line)
        {
            var parts = line.Split(',');

            if (parts.Length != 7)
                throw new FormatException("Invalid line format for FulltimeInstructor.");

            var faculty = Enum.Parse<Faculty>(parts[4]);
            var status = Enum.Parse<InstructorStatus>(parts[5]);
            var salary = decimal.Parse(parts[6]);

            var instructor = new FulltimeInstructor(
                parts[0],
                parts[1], 
                parts[2],
                faculty,
                int.Parse(parts[3]),
                salary
            );
            instructor.ChangeStatus(status);

            return instructor;
        }

    }