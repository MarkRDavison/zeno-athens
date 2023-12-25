namespace mark.davison.athens.shared.queries.Scenarios.FetchProjects;

public class FetchProjectsQueryProcessor : IQueryProcessor<FetchProjectsQueryRequest, FetchProjectsQueryResponse>
{
    private readonly IRepository _repository;

    public FetchProjectsQueryProcessor(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<FetchProjectsQueryResponse> ProcessAsync(FetchProjectsQueryRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        await using (_repository.BeginTransaction())
        {
            var projects = await _repository.GetEntitiesAsync<Project>(
                _ => _.UserId == currentUserContext.CurrentUser.Id,
                cancellationToken);

            return new FetchProjectsQueryResponse
            {
                Value = [.. projects.Select(_ => _.ToDto())]
            };
        }
    }
}
