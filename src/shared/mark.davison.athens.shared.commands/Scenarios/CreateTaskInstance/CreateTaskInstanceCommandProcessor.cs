namespace mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;

public class CreateTaskInstanceCommandProcessor : ICommandProcessor<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>
{
    private readonly IRepository _repository;
    private readonly IEntityDefaulter<TaskInstance> _entityDefaulter;
    private readonly ICreateTaskInstanceCache _createTaskInstanceCache;

    public CreateTaskInstanceCommandProcessor(
        IRepository repository,
        IEntityDefaulter<TaskInstance> entityDefaulter,
        ICreateTaskInstanceCache createTaskInstanceCache
    )
    {
        _repository = repository;
        _entityDefaulter = entityDefaulter;
        _createTaskInstanceCache = createTaskInstanceCache;
    }

    public async Task<CreateTaskInstanceCommandResponse> ProcessAsync(CreateTaskInstanceCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        await using (_repository.BeginTransaction())
        {
            var taskInstance = new TaskInstance
            {
                ProjectId = request.ProjectId ?? _createTaskInstanceCache.DefaultProjectId,
                Title = request.Title
            };

            await _entityDefaulter.DefaultAsync(taskInstance, currentUserContext.CurrentUser);

            var created = await _repository.UpsertEntityAsync(taskInstance, cancellationToken);

            if (created == null)
            {
                return new()
                {
                    Errors = [CommonMessages.ERROR_SAVING]
                };
            }

            return new()
            {
                Value = created.ToDto()
            };
        }
    }
}
