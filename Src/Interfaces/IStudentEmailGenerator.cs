namespace Projects.Src.Interfaces
{
    public interface IStudentEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}
