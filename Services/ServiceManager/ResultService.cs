using Projects.Interfaces;
using Projects.IServices;
using Projects.Models;
using Projects.Models.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Services.ServiceManager
{
    public class ResultService:IresultService
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
        public void  GetResultsByStudent(Student student)
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
    }
}
