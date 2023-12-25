namespace mark.davison.athens.shared.models.dtos.Shared;

public class TaskInstanceDto
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
}
