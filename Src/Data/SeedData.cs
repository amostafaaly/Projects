using Projects.Src.Interfaces;
using Projects.Src.DTOs.StudentDTOs;
using Projects.Src.DTOs.CourseDTOs;
using Projects.Src.DTOs.ExamDTOs;
using Projects.Src.DTOs.ResultDTOs;
using Projects.Src.DTOs.InstructorDTOs;
using static Projects.Src.Shared.Enums;

namespace Projects.Src.Data;

public class SeedData
{
    private readonly IStudentService _studentService;
    private readonly IInstructorService _instructorService;
    private readonly ICourseService _courseService;
    private readonly IExamService _examService;
    private readonly IResultService _resultService;

    public SeedData(IStudentService studentService, IInstructorService instructorService,
        ICourseService courseService, IExamService examService, IResultService resultService)
    {
        _studentService = studentService;
        _instructorService = instructorService;
        _courseService = courseService;
        _examService = examService;
        _resultService = resultService;
    }

    public void Initialize()
    {
        if (_studentService.GetAllStudents().Any() || 
        _courseService.GetAllCourses().Any())
         return;

        var student1 = new CreateStudentDto { FirstName = "Mohamed", LastName = "Ali", Faculty = Faculty.CS, EnrollmentYear = 2022 };
        var student2 = new CreateStudentDto { FirstName = "Sara", LastName = "Hassan", Faculty = Faculty.Engineering, EnrollmentYear = 2021 };
        var student3 = new CreateStudentDto { FirstName = "Omar", LastName = "Youssef", Faculty = Faculty.CS, EnrollmentYear = 2023 };
        var student4 = new CreateStudentDto { FirstName = "Nour", LastName = "Ahmed", Faculty = Faculty.Business, EnrollmentYear = 2022 };

        _studentService.AddStudent(student1);
        _studentService.AddStudent(student2);
        _studentService.AddStudent(student3);
        _studentService.AddStudent(student4);

        var allStudents = _studentService.GetAllStudents();
        var mohamedId = allStudents.First(s => s.Name == "Mohamed Ali").Id;
        var saraId = allStudents.First(s => s.Name == "Sara Hassan").Id;
        var omarId = allStudents.First(s => s.Name == "Omar Youssef").Id;
        var nourId = allStudents.First(s => s.Name == "Nour Ahmed").Id;

        var instructor1 = new CreateFulltimeInstructorDto { FirstName = "Ahmed", LastName = "Hassan", Faculty = Faculty.CS, HiringYear = 2015, MonthlySalary = 8000 };
        var instructor2 = new CreateFulltimeInstructorDto { FirstName = "Khaled", LastName = "Mahmoud", Faculty = Faculty.Engineering, HiringYear = 2010, MonthlySalary = 9000 };
        var instructor3 = new CreateParttimeInstructorDto { FirstName = "Laila", LastName = "Karim", Faculty = Faculty.Business, HiringYear = 2020, HourlyRate = 150, HoursWorked = 40 };

        _instructorService.AddFulltimeInstructor(instructor1);
        _instructorService.AddFulltimeInstructor(instructor2);
        _instructorService.AddParttimeInstructor(instructor3);

        var allInstructors = _instructorService.GetAllInstructors();
        var ahmedInstructorId = allInstructors.First(i => i.Name == "Ahmed Hassan").Id;
        var khaledInstructorId = allInstructors.First(i => i.Name == "Khaled Mahmoud").Id;
        var lailaInstructorId = allInstructors.First(i => i.Name == "Laila Karim").Id;

        var course1 = new CreateCourseDto { Name = "Introduction to Programming", CreditHours = 3, Description = "Introduction to Programming", Faculty = Faculty.CS };
        var course2 = new CreateCourseDto { Name = "Data Structures", CreditHours = 4, Description = "Data Structures", Faculty = Faculty.Engineering };
        var course3 = new CreateCourseDto { Name = "Business Communication", CreditHours = 2, Description = "Business Communication", Faculty = Faculty.Business };

        _courseService.CreateCourse(course1);
        _courseService.CreateCourse(course2);
        _courseService.CreateCourse(course3);

        var allCourses = _courseService.GetAllCourses();
        var course1Id = allCourses.First(c => c.Name == "Introduction to Programming").Id;
        var course2Id = allCourses.First(c => c.Name == "Data Structures").Id;
        var course3Id = allCourses.First(c => c.Name == "Business Communication").Id;

        _courseService.AssignInstructor(course1Id, ahmedInstructorId);
        _courseService.AssignInstructor(course2Id, khaledInstructorId);
        _courseService.AssignInstructor(course3Id, lailaInstructorId);

        _courseService.AssignStudent(mohamedId, course1Id);
        _courseService.AssignStudent(saraId, course2Id);
        _courseService.AssignStudent(omarId, course1Id);
        _courseService.AssignStudent(nourId, course3Id);

        var exam1 = new CreateExamDTO { CourseId = course1Id, Date = DateTime.Now.AddDays(7), TotalMarks = 100 };
        var exam2 = new CreateExamDTO { CourseId = course2Id, Date = DateTime.Now.AddDays(10), TotalMarks = 100 };

        _examService.CreateExam(exam1);
        _examService.CreateExam(exam2);

        var allExams = _examService.GetAllExams();
        var exam1Id = allExams.First(e => e.CourseId == course1Id).Id;
        var exam2Id = allExams.First(e => e.CourseId == course2Id).Id;

        var result1 = new CreateResultDto { StudentId = mohamedId, ExamId = exam1Id, Score = 85 };
        var result2 = new CreateResultDto { StudentId = saraId, ExamId = exam2Id, Score = 90 };
        var result3 = new CreateResultDto { StudentId = omarId, ExamId = exam1Id, Score = 78 };

        _resultService.AddResult(result1);
        _resultService.AddResult(result2);
        _resultService.AddResult(result3);

        Console.WriteLine("  [SEED] Database seeded successfully.");
    }
}
