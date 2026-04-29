namespace Projects.Interfaces
{
    public interface IStudentEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}
