namespace Projects.Src.Utilities.Generators
{
    using Projects.Src.Contracts;

    public class InstructorEmailGenerator : IInstructorEmailGenerator
    {
        // Email format: first initial + last name + @msa.edu.eg
        public string GenerateEmail(string firstName, string lastName)
        {
            var firstInitial = firstName.Trim()[0].ToString();
            var EditedLastname = lastName.Replace(" ", string.Empty);
            return $"{firstInitial.ToLower()}{EditedLastname.ToLower()}@msa.edu.eg";
        }
    }
}