namespace Projects.Src.Models;

    using Projects.Src.Contracts;
    using Projects.Src.Models;

    public abstract class Instructor : User
    {
        public int HiringYear { get; init; }

        public Faculty Faculty { get;  private set; }

        public InstructorStatus Status { get;  set; }

        public Instructor(
            string id,
            string name,
            string universityEmail,
            Faculty faculty,
            int hiringYear)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.", nameof(name));

            if (string.IsNullOrWhiteSpace(universityEmail))
                throw new ArgumentException("Email cannot be empty.", nameof(universityEmail));

            if (hiringYear < 1900 || hiringYear > DateTime.UtcNow.Year)
                throw new ArgumentOutOfRangeException(nameof(hiringYear), "Hiring year is out of valid range.");

            Id = id;
            Name = name;
            UniversityEmail = universityEmail;
            HiringYear = hiringYear;
            Faculty = faculty;
           
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
