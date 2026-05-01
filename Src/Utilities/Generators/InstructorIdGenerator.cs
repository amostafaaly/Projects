namespace Projects.Src.Utilities.Generators
{
    using Projects.Src.Contracts.IGenerator;

    public class InstructorIdGenerator : IInstructorIdGenerator
    {
        // ID format: 1 + last two digits of hiring year + random 3-digit number
        public string GenerateId(int hiringYear)
        {
            var yearPrefix = (hiringYear % 100).ToString("D2");
            var randomPart = Random.Shared.Next(100, 1000);
            return $"1{yearPrefix}{randomPart}";
        }
    }
}