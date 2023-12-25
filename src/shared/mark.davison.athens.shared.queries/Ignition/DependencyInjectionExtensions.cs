using mark.davison.athens.shared.queries.Scenarios.FetchProjects;

namespace mark.davison.athens.shared.queries.Ignition;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection UseQueryCQRS(this IServiceCollection services)
    {
        // TODO: Source gen/reflection to do this as well as create the handlers
        services.AddTransient<IQueryProcessor<FetchTaskInstancesQueryRequest, FetchTaskInstancesQueryResponse>, FetchTaskInstancesQueryProcessor>();
        services.AddTransient<IQueryProcessor<FetchProjectsQueryRequest, FetchProjectsQueryResponse>, FetchProjectsQueryProcessor>();
        return services;
    }
}
