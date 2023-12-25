namespace mark.davison.athens.web.features.Tasks.FetchTaskInstances;

public class FetchTaskInstancesFeatureRequest : IQuery<FetchTaskInstancesFeatureRequest, FetchTaskInstancesFeatureResponse>
{
    public bool ShowCompleted { get; set; }
}
