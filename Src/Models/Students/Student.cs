using Projects.Src.Models.Common;
using Projects.Src.Models.Courses;

namespace Projects.Src.Models.Students
{
	public class Student : User
	{
		public int EnrollmentYear { get; init; }

		public Faculty Faculty { get; private set; }

		public StudentStatus Status { get; private set; }

		public StudentLevel Level { get; private set; }

		

		public Student(
			string firstName,
			string lastName,
			Faculty faculty,
			int enrollmentYear,
			string id,
			string email)
		{
			if (string.IsNullOrWhiteSpace(firstName))
				throw new ArgumentException("First name cannot be empty.", nameof(firstName));

			if (string.IsNullOrWhiteSpace(lastName))
				throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

			if (enrollmentYear < 1900 || enrollmentYear > DateTime.UtcNow.Year)
				throw new ArgumentOutOfRangeException(nameof(enrollmentYear), "Enrollment year is out of valid range.");
			if (string.IsNullOrWhiteSpace(id))
				throw new ArgumentException("ID cannot be empty.", nameof(id));
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException("Email cannot be empty.", nameof(email));

			Id = id;
			Name = $"{firstName.Trim()} {lastName.Trim()}";
			UniversityEmail = email;
			EnrollmentYear = enrollmentYear;
			Faculty = faculty;
			Status = StudentStatus.Active;
			Level = StudentLevel.Undergraduate;
		}

		public void ChangeFaculty(Faculty newFaculty)
		{
			Faculty = newFaculty;
		}

		public void ChangeStatus(StudentStatus newStatus)
		{
			Status = newStatus;
		}

		public void PromoteToGraduate()
		{
			Level = StudentLevel.Graduate;
		}

		public string GetDetails() =>
			$"{Level}: {Name} (ID: {Id}, Faculty: {Faculty}, Status: {Status})";
		public List<Exam> Exams { get; set; } = new List<Exam>();
		public string ToFileLine()
		{
			return $"{Id},{Name},{UniversityEmail},{EnrollmentYear},{Faculty},{Status},{Level}";
        }
        public static Student FromFileLine(string line)
        {
            var parts = line.Split(',');

            if (parts.Length != 7)
                throw new FormatException("Invalid line format for Student.");

            return new Student(
                parts[1],
                "",       
                Enum.Parse<Faculty>(parts[4]),
                int.Parse(parts[3]),
                parts[0],
                parts[2] 
            )
            {
                Status = Enum.Parse<StudentStatus>(parts[5]),
                Level = Enum.Parse<StudentLevel>(parts[6])
            };
        }
    }
}
