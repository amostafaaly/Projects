namespace Projects.Src.Contracts.IGenerator
{
    public interface IStudentEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}
