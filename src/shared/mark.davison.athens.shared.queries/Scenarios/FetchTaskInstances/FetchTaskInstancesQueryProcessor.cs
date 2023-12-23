namespace mark.davison.athens.shared.queries.Scenarios.FetchTaskInstances;

public class FetchTaskInstancesQueryProcessor : IQueryProcessor<FetchTaskInstancesQueryRequest, FetchTaskInstancesQueryResponse>
{
    private readonly IRepository _repository;

    public FetchTaskInstancesQueryProcessor(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<FetchTaskInstancesQueryResponse> ProcessAsync(FetchTaskInstancesQueryRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        await using (_repository.BeginTransaction())
        {
            var tasks = await _repository.GetEntitiesAsync<TaskInstance>(
                _ => _.UserId == currentUserContext.CurrentUser.Id,
                cancellationToken);

            return new FetchTaskInstancesQueryResponse
            {
                Value = [.. tasks.Select(_ => _.ToDto())]
            };
        }
    }
}
