namespace Projects.Src.Utilities.Generators
{
    using Projects.Src.Contracts;

    public class StudentIdGenerator : IStudentIdGenerator
    {
        // ID format: 2 + last two digits of enrollment year + random 3-digit number
        public string GenerateId(int enrollmentYear)
        {
            var yearPrefix = (enrollmentYear % 100).ToString("D2");
            var randomPart = Random.Shared.Next(100, 1000); 
            return $"2{yearPrefix}{randomPart}";
        }
    }
}