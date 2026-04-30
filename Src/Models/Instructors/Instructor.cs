namespace Projects.Src.Models.Instructors
{
    using Projects.Src.Interfaces;
    using Projects.Src.Models.Common;
    using Projects.Src.Models.Courses;

    public abstract class Instructor : User
    {
        public int HiringYear { get; init; }

        public Faculty Faculty { get;  private set; }

        public InstructorStatus Status { get;private  set; }

        public Instructor(
            string firstName,
            string lastName,
            Faculty faculty,
            int hiringYear,
            IInstructorIdGenerator idGenerator,
            IInstructorEmailGenerator emailGenerator)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

            if (hiringYear < 1900 || hiringYear > DateTime.UtcNow.Year)
                throw new ArgumentOutOfRangeException(nameof(hiringYear), "Hiring year is out of valid range.");

            ArgumentNullException.ThrowIfNull(idGenerator);
            ArgumentNullException.ThrowIfNull(emailGenerator);

            Id = idGenerator.GenerateId(hiringYear);
            Name = $"{firstName.Trim()} {lastName.Trim()}";
            UniversityEmail = emailGenerator.GenerateEmail(firstName.Trim(), lastName.Trim());
            HiringYear = hiringYear;
            Faculty = faculty;
            Status = InstructorStatus.FullTime;
        }

        public void ChangeFaculty(Faculty newFaculty)
        {
            Faculty = newFaculty;
        }

        public void ChangeStatus(InstructorStatus newStatus)
        {
            Status = newStatus;
        }
        public abstract decimal CalculateSalary();

        public string GetDetails() =>
            $"Instructor: {Name} (ID: {Id}, Faculty: {Faculty}, Status: {Status})";
        public List<Exam> Exams { get; set; } = new List<Exam>();
        
       
    }
}