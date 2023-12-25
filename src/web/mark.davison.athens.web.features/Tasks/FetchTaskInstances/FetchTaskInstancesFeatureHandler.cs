namespace mark.davison.athens.web.features.Tasks.FetchTaskInstances;

public class FetchTaskInstancesFeatureHandler : IQueryHandler<FetchTaskInstancesFeatureRequest, FetchTaskInstancesFeatureResponse>
{
    private readonly IClientHttpRepository _repository;

    public FetchTaskInstancesFeatureHandler(
        IClientHttpRepository repository
    )
    {
        _repository = repository;
    }

    public async Task<FetchTaskInstancesFeatureResponse> Handle(FetchTaskInstancesFeatureRequest query, CancellationToken cancellation)
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
