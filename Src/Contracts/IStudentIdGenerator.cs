namespace Projects.Src.Contracts
{
    public interface IStudentIdGenerator
    {
        string GenerateId(int enrollmentYear);
    }
}
