namespace Projects.Src.Utilities.Generators
{
    using Projects.Src.Contracts.IGenerator;

    public class StudentEmailGenerator : IStudentEmailGenerator
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