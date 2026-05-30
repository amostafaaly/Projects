using Projects.Src.DTOs.ResultDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;

namespace Projects.Src.Services
{
    public class ResultService : IResultService
    {
        private readonly IRepository<Result> _resultRepo;
        private readonly IRepository<Student> _studentRepo;
        private readonly IRepository<Exam> _examRepo;

        public ResultService(IRepository<Result> resultRepo, IRepository<Student> studentRepo, IRepository<Exam> examRepo)
        { 
            _resultRepo = resultRepo; _studentRepo = studentRepo; _examRepo = examRepo;
        }

        public void AddResult(CreateResultDto dto)
        {
            if (_studentRepo.GetById(dto.StudentId) == null) throw new EntityNotFoundException(nameof(Student), dto.StudentId);
            
            var exam = _examRepo.GetById(dto.ExamId);
            if (exam == null) throw new EntityNotFoundException(nameof(Exam), dto.ExamId);

            if (dto.Score < 0 || dto.Score > exam.TotalMarks)
                throw new SchoolException($"Score ({dto.Score}) must be between 0 and {exam.TotalMarks}.");

            var result = new Result
            {
                Id = Guid.NewGuid().ToString("N")[..8],
                Name = $"Result — {dto.StudentId} on {dto.ExamId}",
                StudentId = dto.StudentId,
                ExamId = dto.ExamId,
                Score = dto.Score,
                TotalMarks = exam.TotalMarks
            };

            _resultRepo.Add(result);
        }
        public IEnumerable<UpdateResultDto> GetAllResults()
        {
            return _resultRepo.GetAll().Select(r => new UpdateResultDto
            {
                StudentId = r.StudentId,
                ExamId = r.ExamId,
                Score = r.Score,
                TotalMarks = r.TotalMarks,
                Percentage = r.Percentage
            });
        }

        public IEnumerable<UpdateResultDto> GetResultsByStudent(string studentId)
        {
            if (_studentRepo.GetById(studentId) == null) throw new EntityNotFoundException(nameof(Student), studentId);

            var results = _resultRepo.Find(r => r.StudentId == studentId).ToList();
            if (!results.Any()) throw new SchoolException($"No exam results found for student '{studentId}'.");

            return results.Select(r => new UpdateResultDto
            {
                StudentId = r.StudentId,
                ExamId = r.ExamId,
                Score = r.Score,
                TotalMarks = r.TotalMarks,
                Percentage = r.Percentage
            });
        }

        public void UpdateResult(string studentId, string examId, int newScore)
        {
            var result = _resultRepo.Find(r => r.StudentId == studentId && r.ExamId == examId).FirstOrDefault();
            if (result == null)
                throw new EntityNotFoundException(nameof(Result), $"{studentId}/{examId}");

            var exam = _examRepo.GetById(examId)
                ?? throw new EntityNotFoundException(nameof(Exam), examId);
            if (newScore < 0 || newScore > exam.TotalMarks)
                throw new SchoolException($"Score ({newScore}) must be between 0 and {exam.TotalMarks}.");

            result.Score = newScore;
            _resultRepo.Update(result);
        }

        public void DeleteResult(string studentId, string examId)
        {
            var result = _resultRepo.Find(r => r.StudentId == studentId && r.ExamId == examId).FirstOrDefault();
            if (result == null)
                throw new EntityNotFoundException("Result", $"{studentId}/{examId}");

            _resultRepo.Remove(result);
        }
    }
}
