namespace Projects.Src.Contracts
{
    public interface IStudentEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}
