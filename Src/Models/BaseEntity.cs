namespace Projects.Src.Models;

public abstract class BaseEntity 
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
