namespace mark.davison.athens.web.features.Task.FetchTaskInstances;

public class FetchTaskInstancesFeatureHandler : ICommandHandler<FetchTaskInstancesFeatureRequest, FetchTaskInstancesFeatureResponse>
{
    private readonly IClientHttpRepository _repository;

    public FetchTaskInstancesFeatureHandler(
        IClientHttpRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<FetchTaskInstancesFeatureResponse> Handle(FetchTaskInstancesFeatureRequest command, CancellationToken cancellation)
    {
        var request = new FetchTaskInstancesQueryRequest();

        var response = await _repository.Get<FetchTaskInstancesQueryResponse, FetchTaskInstancesQueryRequest>(request, cancellation);

        return new FetchTaskInstancesFeatureResponse
        {
            Value = [.. response.Value],
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
