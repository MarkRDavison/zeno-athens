namespace mark.davison.athens.web.features.Project.CreateProject;

// TODO: Should this and its req/resp have command in the name??? naming convention
public class CreateProjectFeatureHandler : ICommandHandler<CreateProjectFeatureRequest, CreateProjectFeatureResponse>
{
    private readonly IStateStore _stateStore;
    private readonly IClientHttpRepository _repository;

    public CreateProjectFeatureHandler(
        IStateStore stateStore,
        IClientHttpRepository repository
    )
    {
        _stateStore = stateStore;
        _repository = repository;
    }

    public async Task<CreateProjectFeatureResponse> Handle(CreateProjectFeatureRequest command, CancellationToken cancellation)
    {
        var request = new CreateProjectCommandRequest
        {
            Name = command.Name,
            Description = command.Description,
            ParentProjectId = command.ParentProjectId
        };

        var response = await _repository.Post<CreateProjectCommandResponse, CreateProjectCommandRequest>(request, cancellation);

        if (response.Success && response.Value != null)
        {
            var existing = _stateStore.GetState<ProjectState>().Instance.Projects;
            _stateStore.SetState(new ProjectState([.. existing, response.Value]));
        }
        else
        {
            Console.Error.WriteLine("CreateProjectCommandRequest: Failed, TODO: TOAST");
        }

        return new CreateProjectFeatureResponse
        {
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
