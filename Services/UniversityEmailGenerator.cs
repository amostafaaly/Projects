namespace Projects.Services
{
    using Projects.Interfaces;

    public class UniversityEmailGenerator : IUniversityEmailGenerator
    {
        // Email format: first name + last name + @msa.edu.eg
        
        public string GenerateEmail(string firstName, string lastName)
        {
            var normalizedFirst = (firstName ?? string.Empty).Replace(" ", string.Empty).ToLower();
            var normalizedLast = (lastName ?? string.Empty).Replace(" ", string.Empty).ToLower();
            return $"{normalizedFirst}{normalizedLast}@msa.edu.eg";
        }
    }
}