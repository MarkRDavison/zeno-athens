namespace mark.davison.athens.shared.models.Entities;

public class Project : AthensEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Colour { get; set; }
    public Guid? ParentProjectId { get; set; }

    public virtual Project? ParentProject { get; set; }
}
