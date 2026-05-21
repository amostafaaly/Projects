namespace Projects.Src.Shared
{
    public class SchoolException : Exception
    {
        public SchoolException(string message) : base(message) { }
    }

    public class EntityNotFoundException : SchoolException
    {
        public EntityNotFoundException(string entityName, string id)
            : base($"{entityName} with ID '{id}' was not found in the database.") { }
    }

    public class DatabaseOperationException : SchoolException
    {
        public DatabaseOperationException(string operation, string details)
            : base($"Database failed during '{operation}' operation. Details: {details}") { }
    }
}
