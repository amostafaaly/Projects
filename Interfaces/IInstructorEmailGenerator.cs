namespace Projects.Interfaces
{
    public interface IInstructorEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}