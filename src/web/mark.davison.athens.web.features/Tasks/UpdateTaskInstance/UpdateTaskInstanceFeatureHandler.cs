namespace mark.davison.athens.web.features.Tasks.UpdateTaskInstance;

public class UpdateTaskInstanceFeatureHandler : ICommandHandler<UpdateTaskInstanceFeatureRequest, UpdateTaskInstanceFeatureResponse>
{
    private readonly IClientHttpRepository _repository;
    private readonly IStateStore _stateStore;

    public UpdateTaskInstanceFeatureHandler(
        IClientHttpRepository repository,
        IStateStore stateStore
    )
    {
        _repository = repository;
        _stateStore = stateStore;
    }

    public async Task<UpdateTaskInstanceFeatureResponse> Handle(UpdateTaskInstanceFeatureRequest command, CancellationToken cancellation)
    {
        var request = new UpdateTaskInstanceCommandRequest
        {
            TaskInstanceId = command.TaskInstanceId,
            Changes = command.Changes
        };
        var response = await _repository.Post<UpdateTaskInstanceCommandResponse, UpdateTaskInstanceCommandRequest>(request, cancellation);

        if (response.Success && response.Value != null)
        {
            var existing = _stateStore.GetState<TaskInstanceListState>().Instance.TaskInstances;
            _stateStore.SetState(new TaskInstanceListState(
                [
                    .. existing.Where(_ => _.Id != command.TaskInstanceId),
                    response.Value
                ]));
        }

        return new UpdateTaskInstanceFeatureResponse
        {
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
