namespace mark.davison.athens.web.features.Tasks.UpdateTaskInstance;

public class UpdateTaskInstanceFeatureRequest : ICommand<UpdateTaskInstanceFeatureRequest, UpdateTaskInstanceFeatureResponse>
{
    public Guid TaskInstanceId { get; set; }
    public List<DiscriminatedPropertyChangeset> Changes { get; set; } = new();
}
