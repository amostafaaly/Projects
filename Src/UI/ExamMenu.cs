using Projects.Src.DTOs.ExamDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Shared;

namespace Projects.Src.UI
{
    public class ExamMenu
    {
        private readonly IExamService _service;

        public ExamMenu(IExamService service)
        {
            _service = service;
        }

        public void Run()
        {
            int choice;
            do
            {
                UIHelper.DrawHeader("Exam Management");
                Console.WriteLine("  1. Create New Exam");
                Console.WriteLine("  2. View All Exams");
                Console.WriteLine("  3. Search Exam by ID");
                Console.WriteLine("  4. Update Exam");
                Console.WriteLine("  5. Delete Exam");
                Console.WriteLine(" -1. Back to Main Menu\n");

                choice = InputValidator.GetValidInt("Option: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            UIHelper.DrawHeader("Create New Exam");
                            var createDto = new CreateExamDTO
                            {
                                CourseId = InputValidator.GetValidString("Course ID: "),
                                Date = InputValidator.GetValidDate("Exam Date (MM/DD/YYYY): "),
                                TotalMarks = InputValidator.GetValidInt("Total Marks: ")
                            };
                            _service.CreateExam(createDto);
                            UIHelper.ShowSuccess("Exam created successfully.");
                            break;

                        case 2:
                            UIHelper.DrawHeader("All Exams");
                            var exams = _service.GetAllExams();
                            if (!exams.Any())
                                UIHelper.ShowWarning("No exams found.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var exam in exams)
                                    Console.WriteLine($"  {exam.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 3:
                            UIHelper.DrawHeader("Search Exam by ID");
                            string searchId = InputValidator.GetValidString("Enter Exam ID: ");
                            var foundExam = _service.GetExamById(searchId);
                            Console.WriteLine($"\n  {foundExam.GetDetails()}");
                            break;

                        case 4:
                            UIHelper.DrawHeader("Update Exam");
                            string updateId = InputValidator.GetValidString("Exam ID to Update: ");
                            var existingExam = _service.GetExamById(updateId);
                            var updateDto = new UpdateExamDto
                            {
                                Id = updateId,
                                Name = InputValidator.GetValidString("New Exam Name: "),
                                CourseId = InputValidator.GetValidString("Course ID: "),
                                Date = InputValidator.GetValidDate("New Exam Date (MM/DD/YYYY): "),
                                TotalMarks = InputValidator.GetValidInt("New Total Marks: ")
                            };
                            _service.UpdateExam(updateDto);
                            UIHelper.ShowSuccess("Exam updated successfully.");
                            break;

                        case 5:
                            UIHelper.DrawHeader("Delete Exam");
                            string deleteId = InputValidator.GetValidString("Enter Exam ID to delete: ");
                            _service.DeleteExam(deleteId);
                            UIHelper.ShowSuccess("Exam deleted successfully.");
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
