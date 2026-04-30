using Projects.Interfaces;
using Projects.Interfaces;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Models.Courses;
using Projects.Src.Models.Courses;
using Projects.Src.Models.Students;
using Projects.Src.Services.FileHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Services.ServiceManager
{
    public class ResultService : IresultService
    {
        private readonly IManager<Result> _resultManager;
        private readonly IManager<Exam> _examManager;

        public ResultService(IManager<Result> resultManager, IManager<Exam> examManager)
        {
            _resultManager = resultManager;
            _examManager = examManager;
        }

        public void AddResult(Student student, Exam exam, int score)
        {
            var result = new Result
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = student.Id,
                ExamId = exam.Id,
                Score = score,
                TotalMarks = exam.TotalMarks
            };

            _resultManager.Add(result);

            Console.WriteLine(
                $"{student.Name} got Grade: {result.Grade}");
        }
        public void GetResultsByStudent(Student student)
        {
            var results = _resultManager.GetAll();
            if (results.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }
            foreach (var result in results)
            {
                var exam = _examManager.GetById(result.ExamId);
                Console.WriteLine($"Exam: {exam.Name}, Score: {result.Score}, Grade: {result.Grade}");
            }

        }
        public void SaveResults()
        {
            var results = _resultManager.GetAll();

            var lines = new List<string>();

            foreach (var r in results)
            {
                lines.Add(r.ToFileLine());
            }

            FileHandler.SaveToFile(lines, "results.txt");
        }
        public void LoadResults()
        {
            var lines = FileHandler.LoadAll("results.txt");

            foreach (var line in lines)
            {
                var result = Result.FromFileLine(line);
                _resultManager.Add(result);
            }
        }
    }
}