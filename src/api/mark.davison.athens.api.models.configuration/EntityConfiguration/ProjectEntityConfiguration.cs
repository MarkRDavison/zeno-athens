namespace mark.davison.athens.api.models.configuration.EntityConfiguration;

public class ProjectEntityConfiguration : AthensEntityConfiguration<Project>
{
    public override void ConfigureEntity(EntityTypeBuilder<Project> builder)
    {
        builder
            .Property(_ => _.Name);

        builder
            .Property(_ => _.Description);

        builder
            .Property(_ => _.Colour);
    }
}
