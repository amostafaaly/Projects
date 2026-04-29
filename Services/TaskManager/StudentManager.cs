namespace Projects.Services
{
	using System.Collections.Generic;
	using Projects.Interfaces;
	using Projects.Models;

	public class StudentManager
	{
		private readonly Dictionary<string, Student> _students = new();
		private readonly IStudentIdGenerator _idGenerator;
		private readonly IStudentEmailGenerator _emailGenerator;

		public StudentManager(
			IStudentIdGenerator idGenerator,
			IStudentEmailGenerator emailGenerator)
		{
			ArgumentNullException.ThrowIfNull(idGenerator);
			ArgumentNullException.ThrowIfNull(emailGenerator);

			_idGenerator = idGenerator;
			_emailGenerator = emailGenerator;
		}

		// to get all student w bardu 3sha mhdsh y2dr y access el dictionary w y3ml 7aga fyh
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

			var baseEmail = _emailGenerator.GenerateEmail(firstName.Trim(), lastName.Trim());
			var uniqueEmail = GenerateUniqueEmail(baseEmail);
			var uniqueId = GenerateUniqueId(enrollmentYear);

			var student = new Student(
				firstName,
				lastName,
				faculty,
				enrollmentYear,
				uniqueId,
				uniqueEmail);

			_students[student.Id] = student;
			return student;
		}

		private string GenerateUniqueEmail(string baseEmail)
		{
			// to check uniqness of the generated mail 
			// lw mwgod fa bzwd 1 ex: ahmedmostafa -> ahmedmostafa1 -> ahmedmostafa2
			var uniqueEmail = baseEmail;
			var suffix = 1;

			while (EmailExists(uniqueEmail))
			{
				uniqueEmail = baseEmail.Replace("@", $"{suffix}@");
				suffix++;
			}

			return uniqueEmail;
		}

		private bool EmailExists(string email)
		{
			foreach (var student in _students.Values)
			{
				if (student.UniversityEmail == email)
					return true;
			}

			return false;
		}

		private string GenerateUniqueId(int enrollmentYear)
		{
			string id;
			do
			{
				id = _idGenerator.GenerateId(enrollmentYear);
			}
			while (_students.ContainsKey(id));

			return id;
		}

		public void PromoteToGraduate(string studentId)
		{
			var student = GetStudent(studentId);
			student.PromoteToGraduate();
		}

		public void ChangeFaculty(string studentId, Faculty newFaculty)
		{
			var student = GetStudent(studentId);
			student.ChangeFaculty(newFaculty);
		}

		public void ChangeStatus(string studentId, StudentStatus newStatus)
		{
			var student = GetStudent(studentId);
			student.ChangeStatus(newStatus);
		}

		public void DeleteStudent(string studentId)
		{
			if (!_students.Remove(studentId))
				throw new KeyNotFoundException($"Student with ID '{studentId}' not found.");
		}

		public Student GetStudent(string id)
		{
			if (_students.ContainsKey(id))
				return _students[id];

			throw new KeyNotFoundException($"Student with ID '{id}' not found.");
		}
	}
}
