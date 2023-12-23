namespace mark.davison.athens.shared.models;

public class AthensEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}