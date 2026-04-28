namespace Projects.Services
{
    using Projects.Interfaces;

    public class InstructorEmailGenerator : IInstructorEmailGenerator
    {
        // Email format: first initial + last name + @msa.edu.eg
        public string GenerateEmail(string firstName, string lastName)
        {
            var firstInitial = string.IsNullOrWhiteSpace(firstName) ? string.Empty : firstName.Trim()[0].ToString();
            var normalizedLastName = (lastName ?? string.Empty).Replace(" ", string.Empty);
            return $"{firstInitial.ToLower()}{normalizedLastName.ToLower()}@msa.edu.eg";
        }
    }
}