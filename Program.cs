using Projects.Src.Services;
using Projects.Src.Utilities.Generators;
using Projects.Src.Utilities.FileHandler;
using Projects.Src.Contracts;
using Projects.Src.Models;
using Projects.Src.Managers;

var studentIdGenerator = new StudentIdGenerator();
var studentEmailGenerator = new StudentEmailGenerator();
var instructorIdGenerator = new InstructorIdGenerator();
var instructorEmailGenerator = new InstructorEmailGenerator();

var studentManager = new StudentManager();
var courseManager = new CourseManager();
var instructorManager = new InstructorManager();
var enrollmentManager = new EnrollmentManager(courseManager, studentManager);
var gradeManager = new GradeManager(courseManager, new GradingService());
var examManager = new ExamManager(courseManager, enrollmentManager);
var resultManager = new ResultManager();

var studentRegistration = new StudentRegistrationService(studentManager, studentIdGenerator, studentEmailGenerator);
var instructorRegistration = new InstructorRegistrationService(instructorManager, instructorIdGenerator, instructorEmailGenerator);
var courseService = new CourseService(courseManager, instructorManager);
var enrollmentService = new EnrollmentService(courseManager, studentManager, gradeManager);
var gradingService = new GradingService();
var resultService = new ResultService(resultManager, examManager, new ResultFileHandler(), gradingService);
var examService = new ExamService(instructorManager, examManager, studentManager, courseManager, enrollmentManager);

// Hena hn3ml Register a Student
Console.WriteLine("=== Student Registration ===");
var student = studentRegistration.RegisterStudent("Mohamed", "Ali", 2020, Faculty.Science);
Console.WriteLine($"Student Registered: {student.Name} (ID: {student.Id})");

// w hena kman Register Instructors
Console.WriteLine("\n=== Instructor Registration ===");
var fullTimeInstructor = instructorRegistration.RegisterFulltimeInstructor("Ahmed", "Hassan", Faculty.Science, 2015, 5000m);
Console.WriteLine($"Full-time Instructor: {fullTimeInstructor.Name} (ID: {fullTimeInstructor.Id})");

// Create el Course
Console.WriteLine("\n=== Course Creation ===");
var course = courseService.CreateCourse(Faculty.Science, "Introduction to Computer Science", 3, "An introductory course on computer science concepts.");
Console.WriteLine($"Course Created: {course.Name} (ID: {course.Id})");

//  Assign Instructor to Course el ehna 3mlnah already
Console.WriteLine("\n=== Assign Instructor to Course ===");
courseService.AssignInstructor(course.Id, fullTimeInstructor.Id);
Console.WriteLine($"Instructor {fullTimeInstructor.Name} assigned to course {course.Name}");

// hena ba enroll Student in Course 
Console.WriteLine("\n=== Enroll Student ===");
enrollmentService.EnrollStudent(course.Id, student.Id);
Console.WriteLine($"{student.Name} enrolled in {course.Name}");

//  Create and Schedule Exam 
Console.WriteLine("\n=== Create and Schedule Exam ===");
var exam = new Exam(DateTime.UtcNow.AddDays(30), 100, course.Id, student.Id, fullTimeInstructor.Id);
exam.Id = Guid.NewGuid().ToString(); // Set exam ID
examManager.Add(exam);
Console.WriteLine($"Exam scheduled for {exam.Date:yyyy-MM-dd} with total marks: {exam.TotalMarks}");

//  Assign Student to Exam w kda kda lazem ykon el student already enrolled in el course 3shan y2dr ykon assigned to el exam
Console.WriteLine("\n=== Assign Student to Exam ===");
examManager.AssignStudentToExam(exam.Id, student.Id);
Console.WriteLine($"Student {student.Name} assigned to exam");

// hena ba record Exam Result
Console.WriteLine("\n=== Record Exam Result ===");
resultService.AddResult(student, exam, 85);


Console.WriteLine("\n=== Student Results ===");
resultService.GetResultsByStudent(student);

// hena ya3ny el mafrod el instructor howa el by3ml Assign Grade ll Student in Course
Console.WriteLine("\n=== Assign Grade to Student ===");
enrollmentService.AssignGrade(course.Id, student.Id, 85);
Console.WriteLine($"Grade 85 assigned to {student.Name} in {course.Name}");

// Calc GPA
Console.WriteLine("\n=== Calculate Student GPA ===");
var gpa = enrollmentService.CalculateStudentGPA(student.Id);
Console.WriteLine($"Student GPA: {gpa}");

// Example 12: Save Results to File
Console.WriteLine("\n=== Save Results ===");
resultService.SaveResults();
Console.WriteLine("Results saved to file");

// Example 13: Load Results from File
Console.WriteLine("\n=== Load Results ===");
resultService.LoadResults();
Console.WriteLine($"Total results loaded: {resultManager.GetAll().Count}");

