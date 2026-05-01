using Projects.Src.Managers;
using Projects.Src.Models;
using Projects.Src.Services;
using Projects.Src.Utilities.Generators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.SeedData
{
    internal class SeedData
    {

        private readonly StudentRegistrationService _studentRegistration;
        private readonly InstructorRegistrationService _instructorRegistration;
        private readonly CourseService _courseService;
        private readonly EnrollmentService _enrollmentService;
        private readonly ResultService _resultService;
        private readonly ExamManager _examManager;

        public SeedData(
            StudentRegistrationService studentRegistration,
            InstructorRegistrationService instructorRegistration,
            CourseService courseService,
            EnrollmentService enrollmentService,
            ResultService resultService,
            ExamManager examManager)
        {
            _studentRegistration = studentRegistration;
            _instructorRegistration = instructorRegistration;
            _courseService = courseService;
            _enrollmentService = enrollmentService;
            _resultService = resultService;
            _examManager = examManager;
        }
        public void Run()
        {
            // Students
            var s1 = _studentRegistration.RegisterStudent("Mohamed", "Ali", 2020, Faculty.Science);
            var s2 = _studentRegistration.RegisterStudent("Sara", "Hassan", 2021, Faculty.Engineering);
            var s3 = _studentRegistration.RegisterStudent("Omar", "Youssef", 2019, Faculty.Science);

            // Instructors
            var i1 = _instructorRegistration.RegisterFulltimeInstructor("Ahmed", "Hassan", Faculty.Science, 2015, 5000m);
            var i2 = _instructorRegistration.RegisterFulltimeInstructor("Khaled", "Mahmoud", Faculty.Engineering, 2010, 7000m);

            // Courses
            var c1 = _courseService.CreateCourse(Faculty.Science, "Intro to CS", 3, "Basics of CS");
            var c2 = _courseService.CreateCourse(Faculty.Engineering, "Data Structures", 4, "DS course");

            // Assign instructors
            _courseService.AssignInstructor(c1.Id, i1.Id);
            _courseService.AssignInstructor(c2.Id, i2.Id);

            // Enroll students
            _enrollmentService.EnrollStudent(c1.Id, s1.Id);
            _enrollmentService.EnrollStudent(c1.Id, s2.Id);
            _enrollmentService.EnrollStudent(c2.Id, s3.Id);


            // Exams + Results
            var exam1 = new Exam(DateTime.UtcNow.AddDays(10), 100)
            {
                Id = Guid.NewGuid().ToString(),
                Name="sss"
            };

            var exam2 = new Exam(DateTime.UtcNow.AddDays(15), 100) 
            {
                Id = Guid.NewGuid().ToString(),
                Name="cs"
            };

            _examManager.Add(exam1);
            _examManager.Add(exam2);

            _resultService.AddResult(s1, exam1, 90);
            _resultService.AddResult(s2, exam1, 80);
            _resultService.AddResult(s3, exam2, 88);

            // Grades
            _enrollmentService.AssignGrade(c1.Id, s1.Id, 90);
            _enrollmentService.AssignGrade(c1.Id, s2.Id, 80);
            _enrollmentService.AssignGrade(c2.Id, s3.Id, 88);

            // Save results
            _resultService.SaveResults();
        }
    }
}
        
    
