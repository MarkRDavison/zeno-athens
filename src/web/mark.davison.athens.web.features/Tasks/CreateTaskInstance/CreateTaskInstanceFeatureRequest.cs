namespace mark.davison.athens.web.features.Tasks.CreateTaskInstance;

public class CreateTaskInstanceFeatureRequest : ICommand<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>
{
    public CreateTaskDto TaskCreateInfo { get; set; } = new() { Valid = false };
}
