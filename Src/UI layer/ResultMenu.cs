using Projects.Src.Managers;
using Projects.Src.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.UI_layer
{
    public class ResultMenu
    {
        private readonly ResultService _resultService;
        private readonly StudentManager _studentManager;
        private readonly ExamManager _examManager;

        public ResultMenu(
            ResultService resultService,
            StudentManager studentManager,
            ExamManager examManager)
        {
            _resultService = resultService;
            _studentManager = studentManager;
            _examManager = examManager;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n--- Result Menu ---");
                Console.WriteLine("1- Add Result");
                Console.WriteLine("2- Get Results by Student");
                Console.WriteLine("3- Save Results");
                Console.WriteLine("4- Load Results");
                Console.WriteLine("-1 Exit");

                int.TryParse(Console.ReadLine(), out int n);
                if (n == -1) break;

                switch (n)
                {
                    case 1:
                        {
                            Console.Write("Student ID: ");
                            var student = _studentManager.GetStudent(Console.ReadLine());

                            Console.Write("Exam ID: ");
                            var exam = _examManager.GetById(Console.ReadLine());

                            Console.Write("Score: ");
                            int score = int.Parse(Console.ReadLine());

                            var result = _resultService.AddResult(student, exam, score);

                            Console.WriteLine($"Result Added: {result.Id}");
                            break;
                        }

                    case 2:
                        {
                            Console.Write("Student ID: ");
                            string id = Console.ReadLine();

                             _resultService.GetResultsByStudent(id);

                          
                            break;
                        }

                    case 3:
                        {
                            _resultService.SaveResults();
                            Console.WriteLine("Results Saved Successfully");
                            break;
                        }

                    case 4:
                        {
                            _resultService.LoadResults();
                            Console.WriteLine("Results Loaded Successfully");
                            break;
                        }
                }
            }
        }
    }
}
