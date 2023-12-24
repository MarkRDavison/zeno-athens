namespace mark.davison.athens.shared.models.Entities;

public class UserOptions : AthensEntity
{
    public Guid? DefaultProjectId { get; set; }
    public virtual Project? DefaultProject { get; set; }
}
