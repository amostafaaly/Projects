namespace Projects.Models
{
	using Projects.Interfaces;

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
			IStudentIdGenerator idGenerator,
			IStudentEmailGenerator emailGenerator)
		{
			if (string.IsNullOrWhiteSpace(firstName))
				throw new ArgumentException("First name cannot be empty.", nameof(firstName));

			if (string.IsNullOrWhiteSpace(lastName))
				throw new ArgumentException("Last name cannot be empty.", nameof(lastName));

			if (enrollmentYear < 1900 || enrollmentYear > DateTime.UtcNow.Year)
				throw new ArgumentOutOfRangeException(nameof(enrollmentYear), "Enrollment year is out of valid range.");

			ArgumentNullException.ThrowIfNull(idGenerator);
			ArgumentNullException.ThrowIfNull(emailGenerator);

			Id = idGenerator.GenerateId(enrollmentYear);
			Name = $"{firstName.Trim()} {lastName.Trim()}";
			UniversityEmail = emailGenerator.GenerateEmail(firstName.Trim(), lastName.Trim());
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
	}
}
