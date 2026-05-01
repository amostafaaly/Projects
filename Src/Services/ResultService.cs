using Projects.Src.Contracts;
using Projects.Src.Models;
using Projects.Src.Utilities.FileHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Services
{
    public class ResultService : IresultService
    {
        private readonly IManager<Result> _resultManager;
        private readonly IManager<Exam> _examManager;
        private readonly IFileHandler<Result> _fileHandler;
        private readonly IGradingService _gradingService;

        public ResultService(IManager<Result> resultManager, IManager<Exam> examManager, IFileHandler<Result> fileHandler, IGradingService gradingService)
        {
            _resultManager = resultManager;
            _examManager = examManager;
            _fileHandler = fileHandler ?? throw new ArgumentNullException(nameof(fileHandler));
            _gradingService = gradingService ?? throw new ArgumentNullException(nameof(gradingService));
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

            var grade = _gradingService.GetLetterGrade(result.Score, result.TotalMarks);
            Console.WriteLine($"{student.Name} got Grade: {grade}");
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
                var grade = _gradingService.GetLetterGrade(result.Score, result.TotalMarks);
                Console.WriteLine($"Exam: {exam.Name}, Score: {result.Score}, Grade: {grade}");
            }

        }
        public void SaveResults()
        {
            var results = _resultManager.GetAll();

            var lines = new List<string>();
            foreach (var r in results)
            {
                lines.Add(_fileHandler.ToFileLine(r));
            }

            FileHandler.SaveToFile(lines, "results.txt");
        }
        public void LoadResults()
        {
            var lines = FileHandler.LoadAll("results.txt");
            foreach (var line in lines)
            {
                var result = _fileHandler.FromFileLine(line);
                _resultManager.Add(result);
            }
        }
    }
}