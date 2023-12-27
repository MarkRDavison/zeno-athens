namespace mark.davison.athens.web.features.Tasks.CreateNewTaskInstance;

public class CreateNewTaskInstanceAction : IAction<CreateNewTaskInstanceAction>
{
    public CreateTaskDto TaskCreateInfo { get; set; } = new() { Valid = false };
}
