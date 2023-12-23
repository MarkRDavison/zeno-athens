namespace mark.davison.athens.api.persistence.EntityDefaulter;

public class GenericAthensEntityDefaulter<TEntity> : IEntityDefaulter<TEntity>
    where TEntity : AthensEntity
{
    private readonly IDateService _dateService;

    public GenericAthensEntityDefaulter(IDateService dateService)
    {
        _dateService = dateService;
    }

    public Task DefaultAsync(TEntity entity, User user)
    {
        if (entity.Id == default(Guid))
        {
            entity.Id = Guid.NewGuid();
        }

        if (entity.UserId == default(Guid))
        {
            entity.UserId = user.Id;
        }

        if (entity.Created == default(DateTime))
        {
            entity.Created = _dateService.Now;
        }

        entity.LastModified = _dateService.Now;

        return Task.CompletedTask;
    }
}
