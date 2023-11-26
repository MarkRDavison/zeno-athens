namespace mark.davison.athens.api.migrations.sqlite;

public class ContextFactory : SqliteDbContextFactory<AthensDbContext>
{
    protected override AthensDbContext DbContextCreation(
            DbContextOptions<AthensDbContext> options
        ) => new AthensDbContext(options);
}
