namespace mark.davison.athens.web.services.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection UseAthensWebServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateService>(new DateService(DateService.DateMode.Local));

        return services;
    }
}
