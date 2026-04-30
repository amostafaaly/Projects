namespace Projects.Src.Interfaces
{
    public interface IInstructorEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}