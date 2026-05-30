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
        // Exit if data already exists to prevent duplicate seeding
        if (_studentService.GetAllStudents().Any() || _courseService.GetAllCourses().Any())
            return;

        //seed students
        _studentService.AddStudent(new CreateStudentDto { FirstName = "Kareem", LastName = "Tarek", Faculty = Faculty.CS, EnrollmentYear = 2023 });
        _studentService.AddStudent(new CreateStudentDto { FirstName = "Laila", LastName = "Mahmoud", Faculty = Faculty.Engineering, EnrollmentYear = 2022 });
        _studentService.AddStudent(new CreateStudentDto { FirstName = "Omar", LastName = "Youssef", Faculty = Faculty.Business, EnrollmentYear = 2024 });
        _studentService.AddStudent(new CreateStudentDto { FirstName = "Salma", LastName = "Ibrahim", Faculty = Faculty.Science, EnrollmentYear = 2023 });

        var allStudents = _studentService.GetAllStudents().ToList();
        var kareemId = allStudents.First(s => s.Name == "Kareem Tarek").Id;
        var lailaId = allStudents.First(s => s.Name == "Laila Mahmoud").Id;
        var omarId = allStudents.First(s => s.Name == "Omar Youssef").Id;
        var salmaId = allStudents.First(s => s.Name == "Salma Ibrahim").Id;

        //seed instructors
        _instructorService.AddFulltimeInstructor(new CreateFulltimeInstructorDto { FirstName = "Ahmed", LastName = "Salem", Faculty = Faculty.CS, HiringYear = 2015, MonthlySalary = 18000 });
        _instructorService.AddFulltimeInstructor(new CreateFulltimeInstructorDto { FirstName = "Mona", LastName = "Hassan", Faculty = Faculty.Science, HiringYear = 2018, MonthlySalary = 16500 });
        _instructorService.AddParttimeInstructor(new CreateParttimeInstructorDto { FirstName = "Mostafa", LastName = "Kamal", Faculty = Faculty.Engineering, HiringYear = 2021, HourlyRate = 250, HoursWorked = 40 });
        _instructorService.AddParttimeInstructor(new CreateParttimeInstructorDto { FirstName = "Shady", LastName = "Ahmed", Faculty = Faculty.Science, HiringYear = 2024, HourlyRate = 150, HoursWorked = 30 });

        var allInstructors = _instructorService.GetAllInstructors().ToList();
        var ahmedId = allInstructors.First(i => i.Name == "Ahmed Salem").Id;
        var monaId = allInstructors.First(i => i.Name == "Mona Hassan").Id;
        var mostafaId = allInstructors.First(i => i.Name == "Mostafa Kamal").Id;
        var shadyId = allInstructors.First(i => i.Name == "Shady Ahmed").Id;

        //seed courses
        _courseService.CreateCourse(new CreateCourseDto { Name = "Enterprise Architecture", CreditHours = 4, Description = "Advanced .NET patterns and scalable architecture.", Faculty = Faculty.CS });
        _courseService.CreateCourse(new CreateCourseDto { Name = "Applied Physics", CreditHours = 3, Description = "Thermodynamics and electromagnetism fundamentals.", Faculty = Faculty.Science });
        _courseService.CreateCourse(new CreateCourseDto { Name = "Engineering Mechanics", CreditHours = 3, Description = "Statics and dynamics of rigid structures.", Faculty = Faculty.Engineering });
        _courseService.CreateCourse(new CreateCourseDto { Name = "Project Management", CreditHours = 3, Description = "Agile workflows, Scrum frameworks, and resource planning.", Faculty = Faculty.Business });

        var allCourses = _courseService.GetAllCourses().ToList();
        var csCourseId = allCourses.First(c => c.Name == "Enterprise Architecture").Id;
        var physCourseId = allCourses.First(c => c.Name == "Applied Physics").Id;
        var mechCourseId = allCourses.First(c => c.Name == "Engineering Mechanics").Id;
        var busCourseId = allCourses.First(c => c.Name == "Project Management").Id;

        //assign instructors to courses
        _courseService.AssignInstructor(csCourseId, ahmedId);
        _courseService.AssignInstructor(physCourseId, monaId);
        _courseService.AssignInstructor(mechCourseId, mostafaId);
        _courseService.AssignInstructor(physCourseId, shadyId);

        //enroll students in courses
        _courseService.AssignStudent(kareemId, csCourseId);
        _courseService.AssignStudent(salmaId, physCourseId);
        _courseService.AssignStudent(lailaId, mechCourseId);
        _courseService.AssignStudent(omarId, busCourseId);
        _courseService.AssignStudent(kareemId, physCourseId);

        //update raw course scores
        _courseService.UpdateCourseRawScore(kareemId, csCourseId, 92.5);
        _courseService.UpdateCourseRawScore(salmaId, physCourseId, 88.0);

        //seed exams
        _examService.CreateExam(new CreateExamDTO { CourseId = csCourseId, Date = DateTime.Now.AddDays(14), TotalMarks = 100 });
        _examService.CreateExam(new CreateExamDTO { CourseId = physCourseId, Date = DateTime.Now.AddDays(20), TotalMarks = 150 });
        _examService.CreateExam(new CreateExamDTO { CourseId = mechCourseId, Date = DateTime.Now.AddDays(10), TotalMarks = 50 });

        var allExams = _examService.GetAllExams().ToList();
        var csExamId = allExams.First(e => e.CourseId == csCourseId).Id;
        var physExamId = allExams.First(e => e.CourseId == physCourseId).Id;
        var mechExamId = allExams.First(e => e.CourseId == mechCourseId).Id;

        //seed results
        _resultService.AddResult(new CreateResultDto { StudentId = kareemId, ExamId = csExamId, Score = 90 });
        _resultService.AddResult(new CreateResultDto { StudentId = salmaId, ExamId = physExamId, Score = 138 });
        _resultService.AddResult(new CreateResultDto { StudentId = kareemId, ExamId = physExamId, Score = 142 });
        _resultService.AddResult(new CreateResultDto { StudentId = lailaId, ExamId = mechExamId, Score = 42 });
    }
}
