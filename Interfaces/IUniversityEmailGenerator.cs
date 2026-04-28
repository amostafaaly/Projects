namespace Projects.Interfaces
{
    public interface IUniversityEmailGenerator
    {
        string GenerateEmail(string firstName, string lastName);
    }
}
