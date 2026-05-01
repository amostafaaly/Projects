using Projects.Src.Contracts.IGenerator;
using Projects.Src.Contracts.IManger;
using Projects.Src.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.Src.Services
{
    public class InstructorRegistrationService
    {
        private readonly IInstructorManager _instructorManager;
        private readonly IInstructorIdGenerator _idGenerator;
        private readonly IInstructorEmailGenerator _emailGenerator;

        public InstructorRegistrationService(
            IInstructorManager instructorManager,
            IInstructorIdGenerator idGenerator,
            IInstructorEmailGenerator emailGenerator)
        {
            _instructorManager = instructorManager ?? throw new ArgumentNullException(nameof(instructorManager));
            _idGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
            _emailGenerator = emailGenerator ?? throw new ArgumentNullException(nameof(emailGenerator));
        }

        public FulltimeInstructor RegisterFulltimeInstructor(
            string firstName,
            string lastName,
            Faculty faculty,
            int hiringYear,
            decimal monthlySalary)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or whitespace.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or whitespace.", nameof(lastName));

            var id = _idGenerator.GenerateId(hiringYear);
            var email = _emailGenerator.GenerateEmail(firstName.Trim(), lastName.Trim());
            var name = $"{firstName.Trim()} {lastName.Trim()}";

            var instructor = new FulltimeInstructor(
                id,
                name,
                email,
                faculty,
                hiringYear,
                monthlySalary);

            _instructorManager.Add(instructor);
            return instructor;
        }

        public ParttimeInstructor RegisterParttimeInstructor(
            string firstName,
            string lastName,
            Faculty faculty,
            int hiringYear,
            decimal hourlyRate,
            int hoursWorked)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name cannot be null or whitespace.", nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last name cannot be null or whitespace.", nameof(lastName));

            var id = _idGenerator.GenerateId(hiringYear);
            var email = _emailGenerator.GenerateEmail(firstName.Trim(), lastName.Trim());
            var name = $"{firstName.Trim()} {lastName.Trim()}";

            var instructor = new ParttimeInstructor(
                id,
                name,
                email,
                faculty,
                hiringYear,
                hourlyRate,
                hoursWorked);

            _instructorManager.Add(instructor);
            return instructor;
        }
    }
}
