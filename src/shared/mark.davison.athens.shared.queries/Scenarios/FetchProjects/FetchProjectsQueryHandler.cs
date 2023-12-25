namespace mark.davison.athens.shared.queries.Scenarios.FetchProjects;

public class FetchProjectsQueryHandler : ValidateAndProcessQueryHandler<FetchProjectsQueryRequest, FetchProjectsQueryResponse>
{
    public FetchProjectsQueryHandler(IQueryProcessor<FetchProjectsQueryRequest, FetchProjectsQueryResponse> processor) : base(processor)
    {
    }
}
