namespace mark.davison.athens.api.persistence;

public class AthensRepository : RepositoryBase<AthensDbContext>
{
    public AthensRepository(
        IDbContextFactory<AthensDbContext> dbContextFactory,
        ILogger<RepositoryBase<AthensDbContext>> logger
    ) : base(
        dbContextFactory,
        logger)
    {
    }
}
