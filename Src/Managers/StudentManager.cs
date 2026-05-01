namespace Projects.Src.Managers
{
    using System.Collections.Generic;
    using Projects.Src.Contracts.IManger;
    using Projects.Src.Models;

    public class StudentManager : IStudentManager
	{
		private readonly Dictionary<string, Student> _students = new();

		// to get all student w bardu 3sha mhdsh y2dr y access el dictionary w y3ml 7aga fyh
		public IEnumerable<Student> AllStudents => _students.Values;

		public void AddStudent(Student student)
		{
			if (student == null)
				throw new ArgumentNullException(nameof(student));

			_students[student.Id] = student;
		}

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
				Guid.NewGuid().ToString(),
				$"{firstName.ToLower()}{lastName.ToLower()}@msa.edu.eg");

			_students[student.Id] = student;
			return student;
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
