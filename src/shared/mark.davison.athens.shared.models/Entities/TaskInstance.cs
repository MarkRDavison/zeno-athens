namespace mark.davison.athens.shared.models.Entities;

public class TaskInstance : AthensEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public bool IsFavourite { get; set; }
    public DateTime? DueTime { get; set; }
    public Guid ProjectId { get; set; }

    public virtual Project? Project { get; set; }
}
