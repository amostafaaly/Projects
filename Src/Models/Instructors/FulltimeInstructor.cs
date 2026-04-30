using Projects.Src.Interfaces;
using Projects.Src.Models.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Projects.Src.Models.Instructors
{
    public class FulltimeInstructor:Instructor
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
        public string ToFileLine()
        {
            return $"{Id},{Name},{UniversityEmail},{HiringYear},{Faculty},{Status},{MonthlySalary}";
        }
        public static FulltimeInstructor FromFileLine(
           string line,
           IInstructorIdGenerator idGenerator,
           IInstructorEmailGenerator emailGenerator)
        {
            var parts = line.Split(',');

            if (parts.Length != 7)
                throw new FormatException("Invalid line format for FulltimeInstructor.");

            var faculty = Enum.Parse<Faculty>(parts[4]);
            var status = Enum.Parse<InstructorStatus>(parts[5]);
            var salary = decimal.Parse(parts[6]);

            var instructor = new FulltimeInstructor(
                parts[1], 
                "",       
                faculty,
                int.Parse(parts[3]),
                idGenerator,
                emailGenerator,
                salary
            );
            instructor.Id = parts[0];
            instructor.ChangeStatus(status);

            return instructor;
        }

    }
}
