using Projects.Src.Contracts.IGenerator;
using Projects.Src.Contracts.IManger;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Services
{
    public class StudentRegistrationService
    {
        private readonly IStudentManager _studentManager;
        private readonly IStudentIdGenerator _idGenerator;
        private readonly IStudentEmailGenerator _emailGenerator;

        public StudentRegistrationService(
            IStudentManager studentManager,
            IStudentIdGenerator idGenerator,
            IStudentEmailGenerator emailGenerator)
        {
            _studentManager = studentManager ?? throw new ArgumentNullException(nameof(studentManager));
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

            // Add the student with the generated ID and email to the manager
            _studentManager.AddStudent(student);
            
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
            foreach (var student in _studentManager.AllStudents)
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
            while (StudentIdExists(id));

            return id;
        }

        private bool StudentIdExists(string id)
        {
            foreach (var student in _studentManager.AllStudents)
            {
                if (student.Id == id)
                    return true;
            }

            return false;
        }
    }
}
