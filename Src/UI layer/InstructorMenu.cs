using Projects.Src.Managers;
using Projects.Src.Models;
using Projects.Src.Services;
using Projects.Src.Utilities.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.UI_layer
{
    public class InstructorMenu
    {
        private readonly InstructorManager instructorManager;
        private readonly InstructorRegistrationService registrationService;
        private readonly InstructorIdGenerator instructorIdGenerator;
        private readonly InstructorEmailGenerator instructorEmailGenerator;

        public InstructorMenu(
            InstructorManager _instructorManager,
            InstructorRegistrationService _registrationService,
            InstructorIdGenerator _instructoridgenerator,
            InstructorEmailGenerator _instructorEmailGenerator
            )
        {
            instructorManager = _instructorManager;
            registrationService = _registrationService;
            instructorIdGenerator = _instructoridgenerator;
            instructorEmailGenerator = _instructorEmailGenerator;
        }

        public void Run()
        {
            Console.WriteLine("1-register FullTime instructor");
            Console.WriteLine("2-register PartialTime instructor");
            Console.WriteLine("3-Remove instructor");
            Console.WriteLine("4-Get instructor By ID");
            Console.WriteLine("5-Display All instructor");
            while (true)
            {
                Console.WriteLine("Enter your choice (or -1 to exit): ");
                int.TryParse(Console.ReadLine(), out int n);
                if (n == -1) break;
                var registrationService = new InstructorRegistrationService(instructorManager, instructorIdGenerator, instructorEmailGenerator);
                switch (n)
                {
                    case 1:
                        Console.Write("First Name: ");
                        string fName = Console.ReadLine();

                        Console.Write("Last Name: ");
                        string lName = Console.ReadLine();

                        Console.Write("Faculty (0 = Cs, 1 = Engineering 2=Arts 3-Business 4-science...): ");
                        Faculty faculty = (Faculty)int.Parse(Console.ReadLine());

                        Console.Write("Hiring Year: ");
                        int year = int.Parse(Console.ReadLine());

                        Console.Write("Monthly Salary: ");
                        decimal salary = decimal.Parse(Console.ReadLine());

                        var fullInstructor = registrationService.RegisterFulltimeInstructor(
                            fName,
                            lName,
                            faculty,
                            year,
                            salary
                        );

                        Console.WriteLine(fullInstructor.GetDetails());
                        break;
                    case 2:
                        Console.Write("First Name: ");
                        string pfName = Console.ReadLine();

                        Console.Write("Last Name: ");
                        string plName = Console.ReadLine();

                        Console.Write("Faculty (0 = Cs, 1 = Engineering 2=Arts 3-Business 4-science...): ");
                        Faculty pfaculty = (Faculty)int.Parse(Console.ReadLine());

                        Console.Write("Hiring Year: ");
                        int pyear = int.Parse(Console.ReadLine());

                        Console.Write("Hourly Rate: ");
                        decimal rate = decimal.Parse(Console.ReadLine());

                        Console.Write("Hours Worked: ");
                        int hours = int.Parse(Console.ReadLine());

                        var partInstructor = registrationService.RegisterParttimeInstructor(
                            pfName,
                            plName,
                            pfaculty,
                            pyear,
                            rate,
                            hours
                        );

                        Console.WriteLine(partInstructor.GetDetails());
                        break;

                    case 3:
                        Console.Write("Instructor ID to remove: ");
                        string removeId = Console.ReadLine();
                        var instructorToRemove = instructorManager.GetById(removeId);
                        break;
                    case 4:
                        Console.WriteLine("Write instructor id to Search");
                        string searchId = Console.ReadLine();
                        var instructor = instructorManager.GetById(searchId);
                        Console.Write(instructor.GetDetails());
                        break;
                    case 5:
                        var allInstructors = instructorManager.GetAll();
                        foreach (var instr in allInstructors)
                        {
                            Console.WriteLine(instr.GetDetails());
                        }
                        break;
                }
            }
        }
    }
}
