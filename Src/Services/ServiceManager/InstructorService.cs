using Projects.Src.Interfaces;

using Projects.Src.Models.Common;
using Projects.Src.Models.Students;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Projects.Services.ServiceManager
{
    public class InstructorService
    {
        private readonly Dictionary<string, Student> _students = new();

        private readonly IStudentIdGenerator _idGenerator;
        private readonly IStudentEmailGenerator _emailGenerator;
        public InstructorService(IStudentIdGenerator idGenerator,
            IStudentEmailGenerator emailGenerator)
        {
            _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
            _emailGenerator = emailGenerator ?? throw new ArgumentNullException(nameof(emailGenerator));
        }

        public Student RegisterStudent(
        string firstName,
        string lastName,
        int enrollmentYear,
        Faculty faculty)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be empty.");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be empty.");

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
    }
}
