namespace mark.davison.athens.api.test.Framework;


public class AthensApiWebApplicationFactory : WebApplicationFactory<Startup>, ICommonWebApplicationFactory<AppSettings>
{
    public IServiceProvider ServiceProvider => base.Services;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, conf) => conf
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.integration.json", false));
        builder.ConfigureTestServices(ConfigureServices);
        builder.ConfigureLogging((WebHostBuilderContext context, ILoggingBuilder loggingBuilder) =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
        });
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services
            .AddHttpClient()
            .AddHttpContextAccessor();

        services.AddScoped<ICurrentUserContext, CurrentUserContext>(_ =>
        {
            var context = new CurrentUserContext();
            if (ModifyCurrentUserContext != null) { ModifyCurrentUserContext(_, context); }
            return context;
        });
    }

    public Action<IServiceProvider, CurrentUserContext>? ModifyCurrentUserContext { get; set; }
}
