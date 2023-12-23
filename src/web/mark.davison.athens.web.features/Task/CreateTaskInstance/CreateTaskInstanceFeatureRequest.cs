namespace mark.davison.athens.web.features.Task.CreateTaskInstance;

public class CreateTaskInstanceFeatureRequest : ICommand<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>
{
    public string Title { get; set; } = string.Empty;
}
