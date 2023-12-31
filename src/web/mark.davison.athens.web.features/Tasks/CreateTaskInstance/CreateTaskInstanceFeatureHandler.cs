namespace mark.davison.athens.web.features.Tasks.CreateTaskInstance;

// TODO: The naming of this isn't great, either needs to be merged with the other create one, or renamed
public class CreateTaskInstanceFeatureHandler : ICommandHandler<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>
{
    private readonly IClientHttpRepository _repository;
    private readonly IStateStore _stateStore;

    public CreateTaskInstanceFeatureHandler(
        IClientHttpRepository repository,
        IStateStore stateStore
    )
    {
        _repository = repository;
        _stateStore = stateStore;
    }

    public async Task<CreateTaskInstanceFeatureResponse> Handle(CreateTaskInstanceFeatureRequest command, CancellationToken cancellation)
    {
        // TODO: Add local version?? optimistic
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = command.TaskCreateInfo.Name,
            ProjectId = command.TaskCreateInfo.ProjectId
        };

        var response = await _repository.Post<CreateTaskInstanceCommandResponse, CreateTaskInstanceCommandRequest>(request, cancellation);

        if (response.Success && response.Value != null)
        {
            var existing = _stateStore.GetState<TaskInstanceListState>().Instance.TaskInstances;
            _stateStore.SetState(new TaskInstanceListState([.. existing, response.Value]));
        }

        return new CreateTaskInstanceFeatureResponse
        {
            Value = response.Value,
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
