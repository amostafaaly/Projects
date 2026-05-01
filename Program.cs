using Projects.Src.Contracts;
using Projects.Src.Managers;
using Projects.Src.Models;
using Projects.Src.SeedData;
using Projects.Src.Services;
using Projects.Src.UI_layer;
using Projects.Src.Utilities.FileHandler;
using Projects.Src.Utilities.Generators;

Console.WriteLine("---------------------------------------------------------------------------------------");
Console.WriteLine("This is Seed Data we Make");
Console.WriteLine("--------------------------");
var studentIdGenerator = new StudentIdGenerator();
var studentEmailGenerator = new StudentEmailGenerator();
var instructorIdGenerator = new InstructorIdGenerator();
var instructorEmailGenerator = new InstructorEmailGenerator();

var studentManager = new StudentManager();
var courseManager = new CourseManager();
var instructorManager = new InstructorManager();
var enrollmentManager = new EnrollmentManager(courseManager, studentManager);
var examManager = new ExamManager();
var examService=new ExamService(instructorManager,examManager,studentManager);
var resultManager = new ResultManager();

var gradingService = new GradingService();
var gradeManager = new GradeManager(courseManager, new GradingService());

var studentRegistration = new StudentRegistrationService(studentManager, studentIdGenerator, studentEmailGenerator);
var instructorRegistration = new InstructorRegistrationService(instructorManager, instructorIdGenerator, instructorEmailGenerator);
var courseService = new CourseService(courseManager, instructorManager);
var enrollmentService = new EnrollmentService(courseManager, studentManager, gradeManager);
var resultService = new ResultService(resultManager, examManager, new ResultFileHandler(), gradingService);
var seed = new SeedData(
    studentRegistration,
    instructorRegistration,
    courseService,
    enrollmentService,
    resultService,
    examManager
);

seed.Run();
Console.WriteLine("-------------------------------------------------------------------------------");
while (true)
{
    Console.WriteLine("1-Make Tasks on instructor");
    Console.WriteLine("2-Make Tasks on Student");
    Console.WriteLine("3-Make Tasks on Course");
    Console.WriteLine("4-Make Tasks on Exam");
    Console.WriteLine("5-Make Tasks on Result");
    Console.WriteLine("-1 to Exits");

    int.TryParse(Console.ReadLine(), out int m);
    if (m == -1) break;
    switch (m)
    {
        case 1:
            try
            {
                var instructorMenu = new InstructorMenu(instructorManager, instructorRegistration, instructorIdGenerator, instructorEmailGenerator);
                instructorMenu.Run();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;

        case 2:
            try
            {
                var studentMenu = new StudentMenu(studentManager);
                studentMenu.Run();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;
        case 3:
            try
            {
                var courseMenu = new CourseMenu(courseService, courseManager, instructorManager);
                courseMenu.Run();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;
        case 4:
            try
            {
                var examMenu = new ExamMenu(examService, examManager, courseManager, studentManager, instructorManager);
                examMenu.Run();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;
        case 5:
            try
            {
                var resultmenu = new ResultMenu(resultService, studentManager, examManager);
                resultmenu.Run();
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            break;

    }
}