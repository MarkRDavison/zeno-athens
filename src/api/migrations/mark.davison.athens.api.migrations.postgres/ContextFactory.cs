namespace mark.davison.athens.api.migrations.postgres;

public class ContextFactory : PostgresDbContextFactory<AthensDbContext>
{
    protected override string ConfigName => "DATABASE";

    protected override AthensDbContext DbContextCreation(
            DbContextOptions<AthensDbContext> options
        ) => new AthensDbContext(options);
}
