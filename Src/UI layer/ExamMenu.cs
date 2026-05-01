using Projects.Src.Managers;
using Projects.Src.Models;
using Projects.Src.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.UI_layer
{
    internal class ExamMenu
    {
        private readonly ExamService _examService;
        private readonly ExamManager _examManager;
        private readonly CourseManager _courseManager;
        private readonly StudentManager _studentManager;
        private readonly InstructorManager _instructorManager;

        public ExamMenu(
            ExamService examService,
            ExamManager examManager,
            CourseManager courseManager,
            StudentManager studentManager,
            InstructorManager instructorManager)
        {
            _examService = examService;
            _examManager = examManager;
            _courseManager = courseManager;
            _studentManager = studentManager;
            _instructorManager = instructorManager;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Exam Menu ---");
                Console.WriteLine("1- Create Exam");
                Console.WriteLine("2- Assign Student to Exam");
                Console.WriteLine("3- Get Exam by ID");
                Console.WriteLine("4- Display All Exams");
                Console.WriteLine("5- Delete Exam");
                Console.WriteLine("-1 Exit");

                int.TryParse(Console.ReadLine(), out int n);
                if (n == -1) break;

                switch (n)
                {
                    case 1:
                        {
                           
                            Console.Write("Total Marks: ");
                            int marks = int.Parse(Console.ReadLine());

                            Console.Write("Days from now: ");
                            int days = int.Parse(Console.ReadLine());
                            Console.WriteLine("Name: ");
                            string name= Console.ReadLine();
                            var exam = new Exam(
                                DateTime.UtcNow.AddDays(days),
                                marks
                             
                               
                            )
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name=name
                            };

                            _examManager.Add(exam);

                            Console.WriteLine("Exam created successfully.");
                            break;
                        }

                    case 2:
                        {
                            Console.Write("Exam ID: ");
                            string examId = Console.ReadLine();

                            Console.Write("Student ID: ");
                            string studentId = Console.ReadLine();
                            Console.WriteLine($"Student {studentId} assigned to exam {examId}");
                            break;
                        }

                    case 3:
                        {
                            Console.Write("Exam ID: ");
                            var exam = _examManager.GetById(Console.ReadLine());

                            Console.WriteLine(exam);
                            break;
                        }

                    case 4:
                        {
                            foreach (var e in _examManager.GetAll())
                                Console.WriteLine(e.GetDetails());
                            break;
                        }

                    case 5:
                        {
                            Console.Write("Exam ID: ");
                            var exam = _examManager.GetById(Console.ReadLine());
                            _examManager.Remove(exam);

                            Console.WriteLine("Exam deleted.");
                            break;
                        }
                }
            }
        }
    }
}
