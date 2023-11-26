namespace mark.davison.athens.api.models.configuration.EntityConfiguration;

public class LabelEntityConfiguration : AthensEntityConfiguration<Label>
{
    public override void ConfigureEntity(EntityTypeBuilder<Label> builder)
    {
        builder
            .Property(_ => _.Name);
    }
}
