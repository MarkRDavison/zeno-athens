namespace mark.davison.athens.api.models.configuration.EntityConfiguration;

public partial class TaskInstanceEntityConfiguration : AthensEntityConfiguration<TaskInstance>
{
    public override void ConfigureEntity(EntityTypeBuilder<TaskInstance> builder)
    {
        builder
            .Property(_ => _.Title);
    }
}
