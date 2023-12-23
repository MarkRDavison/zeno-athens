namespace mark.davison.athens.shared.commands.Ignition;

[ExcludeFromCodeCoverage]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection UseCommandCQRS(this IServiceCollection services)
    {
        // TODO: Source gen/reflection to do this as well as create the handlers
        services.AddTransient<ICommandValidator<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>, CreateTaskInstanceCommandValidator>();
        services.AddTransient<ICommandProcessor<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>, CreateTaskInstanceCommandProcessor>();

        return services;
    }
}
