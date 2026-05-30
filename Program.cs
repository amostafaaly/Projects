using Projects.Src.Data;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Repositories;
using Projects.Src.Services;
using Projects.Src.UI;
using Projects.Src.Utilities;
namespace Projects.Src
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //ensure database is created
                using var context = new ApplicationDbContext();
                context.Database.EnsureCreated();

                //generic repos
                IRepository<Student> studentRepo = new EfRepository<Student>(context);
                IRepository<Instructor> instructorRepo = new EfRepository<Instructor>(context);
                IRepository<Course> courseRepo = new EfRepository<Course>(context);
                IRepository<Exam> examRepo = new EfRepository<Exam>(context);
                IRepository<Result> resultRepo = new EfRepository<Result>(context);
                IRepository<StudentCourse> studentCourseRepo = new EfRepository<StudentCourse>(context);

                //generators
                IIdGenerator<Student> studentIdGen = new StudentIdGenerator();
                IEmailGenerator<Student> studentEmailGen = new StudentEmailGenerator();

                IIdGenerator<Instructor> instructorIdGen = new InstructorIdGenerator();
                IEmailGenerator<Instructor> instructorEmailGen = new InstructorEmailGenerator();

                //services
                IStudentService studentService = new StudentService(studentRepo, studentIdGen, studentEmailGen);
                IInstructorService instructorService = new InstructorService(instructorRepo, instructorIdGen, instructorEmailGen);
                ICourseService courseService = new CourseService(courseRepo, studentCourseRepo, studentRepo);
                IExamService examService = new ExamService(examRepo, courseRepo);
                IResultService resultService = new ResultService(resultRepo, studentRepo, examRepo);

                //seed
                var seed = new SeedData(studentService, instructorService,
                                        courseService, examService, resultService);
                seed.Initialize();

                //menus
                var studentMenu = new StudentMenu(studentService);
                var instructorMenu = new InstructorMenu(instructorService);
                var courseMenu = new CourseMenu(courseService);
                var examMenu = new ExamMenu(examService);
                var resultMenu = new ResultMenu(resultService);

                var mainMenu = new MainMenu(studentService, instructorService,
                                            courseService, examService, resultService);
                mainMenu.Run();

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}