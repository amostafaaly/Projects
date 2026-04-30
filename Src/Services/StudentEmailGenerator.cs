namespace Projects.Src.Services
{
    using Projects.Src.Interfaces;

    public class UniversityEmailGenerator : IStudentEmailGenerator
    {
        // Email format: first name + last name + @msa.edu.eg
        
        public string GenerateEmail(string firstName, string lastName)
        {
            var normalizedFirst = firstName.Replace(" ", string.Empty).ToLower();
            var normalizedLast = lastName.Replace(" ", string.Empty).ToLower();
            return $"{normalizedFirst}{normalizedLast}@msa.edu.eg";
        }
    }
}