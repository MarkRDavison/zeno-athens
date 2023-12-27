namespace mark.davison.athens.shared.models.dtos.Shared;

public class CreateTaskDto
{
    public string Name { get; set; } = string.Empty;
    public string? ProjectName { get; set; }

    public Guid? ProjectId { get; set; }

    public bool Valid { get; set; }
}
