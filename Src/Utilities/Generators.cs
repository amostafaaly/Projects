using Projects.Src.Interfaces;
using Projects.Src.Models;
namespace Projects.Src.Utilities
{
    public class StudentIdGenerator : IIdGenerator<Student>
    {
        public string GenerateId(int enrollmentYear)
            => $"2{(enrollmentYear % 100):D2}{Random.Shared.Next(100, 1000)}";
    }

    public class StudentEmailGenerator : IEmailGenerator<Student>
    {
        public string GenerateEmail(string firstName, string lastName)
            => $"{firstName.Replace(" ", "").ToLower()}{lastName.Replace(" ", "").ToLower()}@msa.edu.eg";
    }

    public class InstructorIdGenerator : IIdGenerator<Instructor>
    {
        public string GenerateId(int hiringYear)
            => $"1{(hiringYear % 100):D2}{Random.Shared.Next(100, 1000)}";
    }

    public class InstructorEmailGenerator : IEmailGenerator<Instructor>
    {
        public string GenerateEmail(string firstName, string lastName)
        {
            var firstInitial = firstName.Trim()[0].ToString();
            string cleanLastName = lastName.Replace(" ", string.Empty);
            return $"{firstInitial.ToLower()}{cleanLastName.ToLower()}@msa.edu.eg";
        }
    }
    public class CourseIdGenerator
    {
        private static int _counter = 1;
        public void SetCounter(int startFrom)
        {
            _counter = startFrom + 1;
        }

        public string GenerateId()
            => $"CRS{_counter++:D3}";
    }

    public class ExamIdGenerator
    {
        private static int _counter = 1;
        public void SetCounter(int startFrom)
        {
            _counter = startFrom + 1;
        }

        public string GenerateId()
            => $"EXM{_counter++:D3}";
    }

    public class ResultIdGenerator
    {
        public string GenerateId()
            => Guid.NewGuid().ToString("N")[..8];
    }
}
