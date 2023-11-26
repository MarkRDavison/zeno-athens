namespace mark.davison.athens.api.models.configuration.EntityConfiguration;

public abstract class AthensEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : AthensEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .ValueGeneratedNever();

        builder
            .Property(_ => _.Created);
        builder
            .Property(_ => _.LastModified);

        builder
            .HasOne(_ => _.User)
            .WithMany()
            .HasForeignKey(_ => _.UserId);

        NavigationPropertyEntityConfigurations.ConfigureEntity(builder);
        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);
}