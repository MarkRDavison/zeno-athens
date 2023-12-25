namespace mark.davison.athens.web.features.Project.FetchProjects;

// TODO: Should this and its req/resp have command in the name??? naming convention
public class FetchProjectsFeatureHandler : ICommandHandler<FetchProjectsFeatureRequest, FetchProjectsFeatureResponse>
{
    private readonly IStateStore _stateStore;
    private readonly IClientHttpRepository _repository;

    public FetchProjectsFeatureHandler(
        IStateStore stateStore,
        IClientHttpRepository repository
    )
    {
        _stateStore = stateStore;
        _repository = repository;
    }

    public async Task<FetchProjectsFeatureResponse> Handle(FetchProjectsFeatureRequest command, CancellationToken cancellation)
    {
        var request = new FetchProjectsQueryRequest { };

        var response = await _repository.Get<FetchProjectsQueryResponse, FetchProjectsQueryRequest>(request, cancellation);

        if (response.Success && response.Value != null)
        {
            _stateStore.SetState(new ProjectState(response.Value));
        }
        else
        {
            Console.Error.WriteLine("FetchProjectsQueryRequest: Failed, TODO: TOAST");
        }

        return new FetchProjectsFeatureResponse
        {
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
