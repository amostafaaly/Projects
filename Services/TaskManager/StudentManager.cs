namespace Projects.Services
{
	using System.Collections.Generic;
	using Projects.Interfaces;
	using Projects.Models;
    using Projects.Services.TaskManager;

    public class StudentManager
	{
		private readonly Dictionary<string, Student> _students = new();
		private readonly IStudentIdGenerator _idGenerator;
		private readonly IUniversityEmailGenerator _emailGenerator;

		public StudentManager(
			IStudentIdGenerator idGenerator,
			IUniversityEmailGenerator emailGenerator)
		{
			ArgumentNullException.ThrowIfNull(idGenerator);
			ArgumentNullException.ThrowIfNull(emailGenerator);

			_idGenerator = idGenerator;
			_emailGenerator = emailGenerator;
		}

		public IEnumerable<Student> AllStudents => _students.Values;

		public Student RegisterStudent(
			string firstName,
			string lastName,
			int enrollmentYear,
			Faculty faculty)
		{
			if (string.IsNullOrWhiteSpace(firstName))
				throw new ArgumentException("First name cannot be null or whitespace.", nameof(firstName));

			if (string.IsNullOrWhiteSpace(lastName))
				throw new ArgumentException("Last name cannot be null or whitespace.", nameof(lastName));

			var student = new Student(
				firstName,
				lastName,
				faculty,
				enrollmentYear,
				_idGenerator,
				_emailGenerator);

			_students[student.Id] = student;
			return student;
		}

		public void PromoteToGraduate(string studentId)
		{
			var student = GetStudent(studentId);
			student.PromoteToGraduate();
		}

		public Student GetStudent(string id)
		{
			if (!_students.TryGetValue(id, out var student))
				throw new KeyNotFoundException($"Student with ID '{id}' not found.");

			return student;
		}
	}
}
