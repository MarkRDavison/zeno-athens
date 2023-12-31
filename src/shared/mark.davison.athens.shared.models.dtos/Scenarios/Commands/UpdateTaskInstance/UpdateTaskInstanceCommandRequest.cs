namespace mark.davison.athens.shared.models.dtos.Scenarios.Commands.UpdateTaskInstance;

[PostRequest(Path = "update-task-instance-command")]
public class UpdateTaskInstanceCommandRequest : ICommand<UpdateTaskInstanceCommandRequest, UpdateTaskInstanceCommandResponse>
{
    public Guid TaskInstanceId { get; set; }
    public List<DiscriminatedPropertyChangeset> Changes { get; set; } = new();
}
