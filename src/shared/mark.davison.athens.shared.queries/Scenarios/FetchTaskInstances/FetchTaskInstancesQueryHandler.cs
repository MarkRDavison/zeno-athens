namespace mark.davison.athens.shared.queries.Scenarios.FetchTaskInstances;

public class FetchTaskInstancesQueryHandler : ValidateAndProcessQueryHandler<FetchTaskInstancesQueryRequest, FetchTaskInstancesQueryResponse>
{
    public FetchTaskInstancesQueryHandler(
        IQueryProcessor<FetchTaskInstancesQueryRequest, FetchTaskInstancesQueryResponse> processor
    ) : base(processor)
    {
    }
}
