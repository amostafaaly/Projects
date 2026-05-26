using Projects.Src.DTOs.ResultDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Shared;

namespace Projects.Src.UI
{
    public class ResultMenu
    {
        private readonly IResultService _service;

        public ResultMenu(IResultService service)
        {
            _service = service;
        }

        public void Run()
        {
            int choice;
            do
            {
                UIHelper.DrawHeader("Result Management");
                Console.WriteLine("  1. Add Result");
                Console.WriteLine("  2. View All Results");
                Console.WriteLine("  3. View Results by Student");
                Console.WriteLine("  4. Update Result Score");
                Console.WriteLine("  5. Delete Result");
                Console.WriteLine(" -1. Back to Main Menu\n");

                choice = InputValidator.GetValidInt("Option: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            UIHelper.DrawHeader("Add Result");
                            var createDto = new CreateResultDto
                            {
                                StudentId = InputValidator.GetValidString("Student ID: "),
                                ExamId = InputValidator.GetValidString("Exam ID: "),
                                Score = InputValidator.GetValidInt("Score: ")
                            };
                            _service.AddResult(createDto);
                            UIHelper.ShowSuccess("Result added successfully.");
                            break;

                        case 2:
                            UIHelper.DrawHeader("All Results");
                            var results = _service.GetAllResults();
                            if (!results.Any())
                                UIHelper.ShowWarning("No results found.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var result in results)
                                    Console.WriteLine($"  {result.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 3:
                            UIHelper.DrawHeader("View Results by Student");
                            string studentId = InputValidator.GetValidString("Enter Student ID: ");
                            var studentResults = _service.GetResultsByStudent(studentId);
                            if (!studentResults.Any())
                                UIHelper.ShowWarning("No results found for that student.");
                            else
                            {
                                UIHelper.PrintDivider();
                                foreach (var result in studentResults)
                                    Console.WriteLine($"  {result.GetDetails()}");
                                UIHelper.PrintDivider();
                            }
                            break;

                        case 4:
                            UIHelper.DrawHeader("Update Result Score");
                            string updateStudentId = InputValidator.GetValidString("Student ID: ");
                            string updateExamId = InputValidator.GetValidString("Exam ID: ");
                            int newScore = InputValidator.GetValidInt("New Score: ");
                            _service.UpdateResult(updateStudentId, updateExamId, newScore);
                            UIHelper.ShowSuccess("Result score updated successfully.");
                            break;

                        case 5:
                            UIHelper.DrawHeader("Delete Result");
                            string deleteStudentId = InputValidator.GetValidString("Student ID: ");
                            string deleteExamId = InputValidator.GetValidString("Exam ID: ");
                            _service.DeleteResult(deleteStudentId, deleteExamId);
                            UIHelper.ShowSuccess("Result deleted successfully.");
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
