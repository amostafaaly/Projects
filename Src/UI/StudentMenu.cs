using Projects.Src.DTOs.StudentDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Shared;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.UI
{
    public class StudentMenu
    {
        private readonly IStudentService _service;

        public StudentMenu(IStudentService service)
        {
            _service = service;
        }

        public void Run()
        {
            int choice;
            do
            {
                UIHelper.DrawHeader("Student Management");
                Console.WriteLine("  1. Add New Student");
                Console.WriteLine("  2. View All Students");
                Console.WriteLine("  3. Search Student by ID");
                Console.WriteLine("  4. Search Students by Faculty");
                Console.WriteLine("  5. Update Student Details");
                Console.WriteLine("  6. Delete Student");
                Console.WriteLine(" -1. Back to Main Menu\n");

                choice = InputValidator.GetValidInt("Option: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            UIHelper.DrawHeader("Add New Student");
                            var createDto = new CreateStudentDto
                            {
                                FirstName = InputValidator.GetValidString("First Name: "),
                                LastName = InputValidator.GetValidString("Last Name: "),
                                EnrollmentYear = InputValidator.GetValidInt("Enrollment Year (e.g. 2024): "),
                                Faculty = (Faculty)InputValidator.GetValidInt("Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): ")
                            };
                            _service.AddStudent(createDto);
                            UIHelper.ShowSuccess("Student added successfully.");
                            break;

                        case 2:
                            UIHelper.DrawHeader("All Enrolled Students");
                            var students = _service.GetAllStudents();
                            if (!students.Any()) UIHelper.ShowWarning("No students found.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var s in students)
                                    Console.WriteLine($"  {s.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 3:
                            UIHelper.DrawHeader("Search Student by ID");
                            string searchId = InputValidator.GetValidString("Enter Student ID: ");
                            var foundStudent = _service.GetStudentById(searchId);
                            if (foundStudent == null)
                                throw new EntityNotFoundException("Student", searchId);
                            Console.WriteLine($"\n  {foundStudent.GetDetails()}");
                            break;

                        case 4:
                            UIHelper.DrawHeader("Search Students by Faculty");
                            Faculty searchFaculty = (Faculty)InputValidator.GetValidInt("Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): ");
                            var facultyStudents = _service.SearchStudentsByFaculty(searchFaculty);
                            if (!facultyStudents.Any())
                                UIHelper.ShowWarning($"No active students found in {searchFaculty}.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var s in facultyStudents)
                                    Console.WriteLine($"  {s.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 5:
                            UIHelper.DrawHeader("Update Student Details");
                            var updateDto = new UpdateStudentDto
                            {
                                Id = InputValidator.GetValidString("Student ID to Update: "),
                                Name = InputValidator.GetValidString("New Full Name: "),
                                Faculty = (Faculty)InputValidator.GetValidInt("New Faculty (0=CS, 1=Eng, 2=Arts, 3=Bus, 4=Sci): "),
                                Status = (StudentStatus)InputValidator.GetValidInt("New Status (0=Active, 1=Graduate, 2=Withdrawn): "),
                                Level = (StudentLevel)InputValidator.GetValidInt("New Level (0=Freshman, 1=Sophomore, 2=Junior, 3=Senior): ")
                            };
                            _service.UpdateStudent(updateDto);
                            UIHelper.ShowSuccess("Student updated successfully.");
                            break;

                        case 6:
                            UIHelper.DrawHeader("Delete Student");
                            string deleteId = InputValidator.GetValidString("Enter Student ID to delete: ");
                            _service.DeleteStudent(deleteId);
                            UIHelper.ShowSuccess("Student removed successfully.");
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
