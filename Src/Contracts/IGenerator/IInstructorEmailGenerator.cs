namespace Projects.Src.Contracts.IGenerator
{
    public interface IInstructorEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}