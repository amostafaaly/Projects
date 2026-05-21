namespace Projects.Src.Interfaces
{
    public interface IIdGenerator<T> where T : class
    {
        string GenerateId(int year);
    }

    public interface IEmailGenerator<T> where T : class
    {
        string GenerateEmail(string firstName, string lastName);
    }
}
