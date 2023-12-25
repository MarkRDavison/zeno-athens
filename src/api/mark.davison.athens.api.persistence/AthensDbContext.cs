namespace mark.davison.athens.api.persistence;

public class AthensDbContext : DbContext
{
    public AthensDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfiguration).Assembly);
    }

    public DbSet<TaskInstance> TaskInstances => Set<TaskInstance>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<UserOptions> UserOptions => Set<UserOptions>();
}
