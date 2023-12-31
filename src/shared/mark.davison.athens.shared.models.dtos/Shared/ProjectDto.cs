namespace mark.davison.athens.shared.models.dtos.Shared;

public class ProjectDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Colour { get; set; } = string.Empty;
}
