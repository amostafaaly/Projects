namespace Projects.Src.Contracts
{
    public interface IInstructorEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}