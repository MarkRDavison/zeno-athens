namespace mark.davison.athens.shared.models.Entities;

public class TaskInstance : AthensEntity
{
    public string Title { get; set; } = string.Empty;

    public Guid ProjectId { get; set; }

    public virtual Project? Project { get; set; }
}
