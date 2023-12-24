namespace mark.davison.athens.shared.models.Entities;

public class Project : AthensEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Colour { get; set; }
    public Guid? ParentId { get; set; }

    public virtual Project? Parent { get; set; }
}
