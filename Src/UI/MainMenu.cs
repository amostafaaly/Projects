using Projects.Src.Interfaces;
using Projects.Src.Shared;

namespace Projects.Src.UI
{
    public class MainMenu
    {
        private readonly IStudentService _studentService;
        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;
        private readonly IExamService _examService;
        private readonly IResultService _resultService;

        public MainMenu(
            IStudentService studentService,
            IInstructorService instructorService,
            ICourseService courseService,
            IExamService examService,
            IResultService resultService)
        {
            _studentService = studentService;
            _instructorService = instructorService;
            _courseService = courseService;
            _examService = examService;
            _resultService = resultService;
        }

        public void Run()
        {
            int choice;
            do
            {
                UIHelper.DrawHeader("University Management System");
                Console.WriteLine("  1. Student Management");
                Console.WriteLine("  2. Instructor Management");
                Console.WriteLine("  3. Course Management");
                Console.WriteLine("  4. Exam Management");
                Console.WriteLine("  5. Result Management");
                Console.WriteLine(" -1. Exit\n");

                choice = InputValidator.GetValidInt("Option: ");

                try
                {
                    switch (choice)
                    {
                        case 1:
                            var studentMenu = new StudentMenu(_studentService);
                            studentMenu.Run();
                            break;

                        case 2:
                            var instructorMenu = new InstructorMenu(_instructorService);
                            instructorMenu.Run();
                            break;

                        case 3:
                            var courseMenu = new CourseMenu(_courseService);
                            courseMenu.Run();
                            break;

                        case 4:
                            var examMenu = new ExamMenu(_examService);
                            examMenu.Run();
                            break;

                        case 5:
                            var resultMenu = new ResultMenu(_resultService);
                            resultMenu.Run();
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
