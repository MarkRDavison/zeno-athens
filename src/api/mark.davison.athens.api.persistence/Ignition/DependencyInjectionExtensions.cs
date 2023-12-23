namespace mark.davison.athens.api.persistence.Ignition;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection UseAthensPersistence(this IServiceCollection services)
    {
        services.AddTransient(typeof(IEntityDefaulter<>), typeof(GenericAthensEntityDefaulter<>));
        services.AddTransient<IEntityDefaulter<User>, UserDefaulter>();
        return services;
    }
}
