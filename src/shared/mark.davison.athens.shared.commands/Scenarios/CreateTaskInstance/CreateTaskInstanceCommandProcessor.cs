namespace mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;

public class CreateTaskInstanceCommandProcessor : ICommandProcessor<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>
{
    private readonly IRepository _repository;
    private readonly IEntityDefaulter<TaskInstance> _entityDefaulter;

    public CreateTaskInstanceCommandProcessor(
        IRepository repository,
        IEntityDefaulter<TaskInstance> entityDefaulter
    )
    {
        _repository = repository;
        _entityDefaulter = entityDefaulter;
    }

    public async Task<CreateTaskInstanceCommandResponse> ProcessAsync(CreateTaskInstanceCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        await using (_repository.BeginTransaction())
        {
            var taskInstance = new TaskInstance
            {
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
