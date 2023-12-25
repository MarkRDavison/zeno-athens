namespace mark.davison.athens.shared.commands.Scenarios.CreateProject;

public class CreateProjectCommandProcessor : ICommandProcessor<CreateProjectCommandRequest, CreateProjectCommandResponse>
{
    private readonly IRepository _repository;
    private readonly IEntityDefaulter<Project> _entityDefaulter;

    public CreateProjectCommandProcessor(
        IRepository repository,
        IEntityDefaulter<Project> entityDefaulter
    )
    {
        _repository = repository;
        _entityDefaulter = entityDefaulter;
    }

    public async Task<CreateProjectCommandResponse> ProcessAsync(CreateProjectCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        await using (_repository.BeginTransaction())
        {
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                ParentProjectId = request.ParentProjectId
            };

            await _entityDefaulter.DefaultAsync(project, currentUserContext.CurrentUser);

            var persisted = await _repository.UpsertEntityAsync(project, cancellationToken);

            // TODO: Helper for persistence failure?
            if (persisted == null)
            {
                return new()
                {
                    Errors = [CommonMessages.ERROR_SAVING]
                };
            }

            return new()
            {
                Value = persisted.ToDto()
            };
        }
    }
}
