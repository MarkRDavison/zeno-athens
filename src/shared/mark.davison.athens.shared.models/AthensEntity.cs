using mark.davison.common.server.abstractions;
using mark.davison.common.server.abstractions.Identification;

namespace mark.davison.athens.shared.models;

public class AthensEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public virtual User? User { get; set; }
}