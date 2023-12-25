namespace mark.davison.athens.web.features.Tasks.CreateTaskInstance;

public class CreateTaskInstanceFeatureRequest : ICommand<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>
{
    public string Title { get; set; } = string.Empty;
}
