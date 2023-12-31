namespace mark.davison.athens.shared.commands.Scenarios.UpdateTaskInstance;

public class UpdateTaskInstanceCommandProcessor : ICommandProcessor<UpdateTaskInstanceCommandRequest, UpdateTaskInstanceCommandResponse>
{
    private readonly IRepository _repository;

    public UpdateTaskInstanceCommandProcessor(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<UpdateTaskInstanceCommandResponse> ProcessAsync(UpdateTaskInstanceCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        await using (_repository.BeginTransaction())
        {
            var existing = await _repository.GetEntityAsync<TaskInstance>(request.TaskInstanceId, cancellationToken);

            if (existing == null)
            {
                return new()
                {
                    Errors =
                    [
                        CommonMessages.FormatMessageParameters(
                            CommonMessages.FAILED_TO_FIND_ENTITY,
                            nameof(TaskInstance),
                            request.TaskInstanceId.ToString())
                    ]
                };
            }

            var taskType = typeof(TaskInstance);

            foreach (var cs in request.Changes)
            {
                var prop = taskType.GetProperty(cs.Name);

                if (prop == null || prop.PropertyType.FullName != cs.PropertyType) { continue; }

                prop.SetValue(existing, cs.Value);
            }

            var updated = await _repository.UpsertEntityAsync(existing, cancellationToken);

            if (updated == null)
            {
                return new()
                {
                    Errors = [CommonMessages.ERROR_SAVING]
                };
            }

            return new() { Value = existing.ToDto() };
        }
    }
}