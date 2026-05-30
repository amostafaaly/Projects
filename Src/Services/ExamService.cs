using Projects.Src.DTOs.ExamDTOs;
using Projects.Src.Interfaces;
using Projects.Src.Models;
using Projects.Src.Shared;
using Projects.Src.Utilities;
namespace Projects.Src.Services
{
    public class ExamService : IExamService
    {
        private readonly IRepository<Exam> _examRepo;
        private readonly IRepository<Course> _courseRepo;
        private readonly ExamIdGenerator _idGenerator = new();

        public ExamService(IRepository<Exam> examRepo, IRepository<Course> courseRepo)
        {
            _examRepo = examRepo;
            _courseRepo = courseRepo;
            var allExams = _examRepo.GetAll().ToList();
            if (allExams.Any())
            {
                var maxId = allExams
                    .Max(e => int.TryParse(e.Id.Replace("EXM", ""), out int n) ? n : 0);
                _idGenerator.SetCounter(maxId);
            }
        }

        public void CreateExam(CreateExamDTO dto)
        {
            var course = _courseRepo.GetById(dto.CourseId);
            if (course == null) throw new EntityNotFoundException("Course", dto.CourseId);
            if (dto.TotalMarks <= 0) throw new SchoolException("Total marks must be a positive number.");

            var exam = new Exam
            {
                Id = _idGenerator.GenerateId(),
                CourseId = dto.CourseId,
                Date = dto.Date,
                TotalMarks = dto.TotalMarks,
                Name = $"Exam — {course.Name}"
            };

            _examRepo.Add(exam);
        }

        public IEnumerable<UpdateExamDto> GetAllExams()
        {
            return _examRepo.GetAll().OrderBy(e => e.Date).Select(e => new UpdateExamDto
            {
                Id = e.Id,
                Name = e.Name,
                CourseId = e.CourseId,
                Date = e.Date,
                TotalMarks = e.TotalMarks
            });
        }
        public UpdateExamDto GetExamById(string id)
        {
            var exam = _examRepo.GetById(id);
            if (exam == null) throw new EntityNotFoundException(nameof(Exam), id);

            return new UpdateExamDto
            {
                Id = exam.Id,
                Name = exam.Name,
                CourseId = exam.CourseId,
                Date = exam.Date,
                TotalMarks = exam.TotalMarks
            };
        }
        public void UpdateExam(UpdateExamDto dto)
        {
            var exam = _examRepo.GetById(dto.Id);
            if (exam == null) throw new EntityNotFoundException("Exam", dto.Id);
            if (_courseRepo.GetById(dto.CourseId) == null) throw new EntityNotFoundException("Course", dto.CourseId);


            if (dto.TotalMarks <= 0)
                throw new SchoolException("Total marks must be a positive number.");

            exam.Name = dto.Name;
            exam.CourseId = dto.CourseId;
            exam.Date = dto.Date;
            exam.TotalMarks = dto.TotalMarks;

            _examRepo.Update(exam);
        }

        public void DeleteExam(string id)
        {
            var exam = _examRepo.GetById(id);
            if (exam == null) throw new EntityNotFoundException("Exam", id);
            _examRepo.Remove(exam);
        }
    }
}
