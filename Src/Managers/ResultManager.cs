using Projects.Src.Contracts;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Managers
{
    public class ResultManager : GenericManager<Result>, IResultManager
    {
        private static int _resultCounter = 0;

        private readonly IExamManager? _examManager;

        public ResultManager()
        {
            _examManager = null;
        }

        public ResultManager(IExamManager examManager)
        {
            _examManager = examManager ?? throw new ArgumentNullException(nameof(examManager));
        }

        public void RecordExamResult(string examId, string studentId, int score)
        {
            if (string.IsNullOrWhiteSpace(examId))
                throw new ArgumentException("Exam ID cannot be empty.", nameof(examId));

            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("Student ID cannot be empty.", nameof(studentId));

            if (score < 0 || score > 100)
                throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 100.");

            if (_examManager != null)
            {
                var exam = _examManager.GetById(examId);
                if (exam == null)
                    throw new KeyNotFoundException($"Exam with ID '{examId}' not found.");

                if (exam.StudentId != studentId)
                    throw new InvalidOperationException($"Student '{studentId}' is not assigned to exam '{examId}'.");
            }

            var result = new Result
            {
                Id = (_resultCounter++).ToString(),
                ExamId = examId,
                StudentId = studentId,
                Score = score,
                TotalMarks = 100
            };

            Add(result);
        }
    }
}
