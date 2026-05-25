using Projects.Src.DTOs.InstructorDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.UI
{
    public class InstructorMenu
    {
        private readonly IInstructorService _service;

        public InstructorMenu(IInstructorService service)
        {
            _service = service;
        }

        public void Run()
        {
            int choice;
            do
            {
                UIHelper.DrawHeader("Instructor Management");
                Console.WriteLine("  1. Add Full-Time Instructor");
                Console.WriteLine("  2. Add Part-Time Instructor");
                Console.WriteLine("  3. View All Instructors");
                Console.WriteLine("  4. Search Instructor by ID");
                Console.WriteLine("  5. Search Instructor by Name");
                Console.WriteLine("  6. Update Instructor");
                Console.WriteLine("  7. Delete Instructor");
                Console.WriteLine(" -1. Back to Main Menu\n");

                choice = InputValidator.GetValidInt("Option: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            UIHelper.DrawHeader("Add Full-Time Instructor");
                            var ftDto = new CreateFulltimeInstructorDto
                            {
                                FirstName = InputValidator.GetValidString("First Name: "),
                                LastName = InputValidator.GetValidString("Last Name: "),
                                HiringYear = InputValidator.GetValidInt("Hiring Year (e.g. 2022): "),
                                Faculty = (Faculty)InputValidator.GetValidInt("Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): "),
                                MonthlySalary = InputValidator.GetValidDecimal("Monthly Salary: ")
                            };
                            _service.AddFulltimeInstructor(ftDto);
                            UIHelper.ShowSuccess("Full-Time Instructor added successfully.");
                            break;

                        case 2:
                            UIHelper.DrawHeader("Add Part-Time Instructor");
                            var ptDto = new CreateParttimeInstructorDto
                            {
                                FirstName = InputValidator.GetValidString("First Name: "),
                                LastName = InputValidator.GetValidString("Last Name: "),
                                HiringYear = InputValidator.GetValidInt("Hiring Year (e.g. 2023): "),
                                Faculty = (Faculty)InputValidator.GetValidInt("Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): "),
                                HourlyRate = InputValidator.GetValidDecimal("Hourly Rate: "),
                                HoursWorked = InputValidator.GetValidInt("Hours Worked This Month: ")
                            };
                            _service.AddParttimeInstructor(ptDto);
                            UIHelper.ShowSuccess("Part-Time Instructor added successfully.");
                            break;

                        case 3:
                            UIHelper.DrawHeader("All Instructors");
                            var instructors = _service.GetAllInstructors();
                            if (!instructors.Any())
                                UIHelper.ShowWarning("No instructors found.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var inst in instructors)
                                    Console.WriteLine($"  {inst.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 4:
                            UIHelper.DrawHeader("Search Instructor by ID");
                            string searchId = InputValidator.GetValidString("Enter Instructor ID: ");
                            var foundById = _service.GetInstructorById(searchId);
                            if (foundById == null)
                                throw new EntityNotFoundException("Instructor", searchId);
                            Console.WriteLine($"\n  {foundById.GetDetails()}");
                            break;

                        case 5:
                            UIHelper.DrawHeader("Search Instructor by Name");
                            string keyword = InputValidator.GetValidString("Enter Name Keyword: ");
                            var foundByName = _service.SearchByName(keyword);
                            if (!foundByName.Any())
                                UIHelper.ShowWarning("No instructors matched that name.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var inst in foundByName)
                                    Console.WriteLine($"  {inst.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 6:
                            UIHelper.DrawHeader("Update Instructor");
                            string updateId = InputValidator.GetValidString("Instructor ID to Update: ");

                            var existingInst = _service.GetInstructorById(updateId);
                            if (existingInst == null)
                                throw new EntityNotFoundException("Instructor", updateId);

                            string newName = InputValidator.GetValidString("New Full Name: ");
                            Faculty newFaculty = (Faculty)InputValidator.GetValidInt("New Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): ");

                            UpdateInstructorDto updateDto;

                            if (existingInst is FulltimeInstructor)
                            {
                                updateDto = new FulltimeInstructorUpdateDto
                                {
                                    Id = updateId,
                                    Name = newName,
                                    Faculty = newFaculty
                                };
                            }
                            else
                            {
                                updateDto = new ParttimeInstructorUpdateDto
                                {
                                    Id = updateId,
                                    Name = newName,
                                    Faculty = newFaculty
                                };
                            }

                            _service.UpdateInstructor(updateDto);
                            UIHelper.ShowSuccess("Instructor updated successfully.");
                            break;

                        case 7:
                            UIHelper.DrawHeader("Delete Instructor");
                            string deleteId = InputValidator.GetValidString("Enter Instructor ID: ");
                            _service.DeleteInstructor(deleteId);
                            UIHelper.ShowSuccess("Instructor removed.");
                            break;
                    }
                }
                catch (SchoolException ex)
                {
                    UIHelper.ShowError(ex.Message);
                }
                catch (Exception ex)
                {
                    UIHelper.ShowError($"An unexpected error occurred: {ex.Message}");
                }

                if (choice != -1) 
                    UIHelper.PressAnyKeyToContinue();

            } while (choice != -1);
        }
    }
}
