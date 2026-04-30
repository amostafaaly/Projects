namespace Projects.Src.Models.Common
{
    public abstract class User
    {
        public string Id { get; protected set; } = string.Empty;

        public string Name { get; protected set; } = string.Empty;

        public string UniversityEmail { get; protected set; } = string.Empty;

        protected User()
        {
        }
    }
}
