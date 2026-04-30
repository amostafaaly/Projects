using Projects.Services.ServiceManager;
using Projects.Src.Interfaces;
using Projects.Src.Models.Common;
using Projects.Src.Models.Courses;
using Projects.Src.Models.Students;
using Projects.Src.Services.TaskManager;
Console.WriteLine("Hello, World!");
var genetateid=Guid.NewGuid().ToString();
var student = new Student  ("Mohamed","ali",Faculty.Science,2020,genetateid,"aal");
var course = new Course()
{
    Id="CS101",
    Name="Introduction to Computer Science",
    CreditHours=3,
    Description="An introductory course on computer science concepts.",
    Faculty=Faculty.Science

};
var exam = new Exam(DateTime.UtcNow.AddDays(30), 100, course.Id, student.Id, "instructorId");
var exammanager=new ExamManager();
var resultmanager=new ResultManager();
var resultService = new ResultService(resultmanager,exammanager);
resultService.AddResult(student, exam, 70);
Console.WriteLine(resultmanager.GetAll().Count);


resultService.LoadResults();
Console.WriteLine(resultmanager.GetAll().Count);
