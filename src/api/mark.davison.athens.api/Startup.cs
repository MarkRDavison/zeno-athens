﻿namespace mark.davison.athens.api;

[UseCQRSServer(typeof(DtosRootType), typeof(CommandsRootType), typeof(QueriesRootType))]
public class Startup
{
    public IConfiguration Configuration { get; }

    public AppSettings AppSettings { get; set; } = null!;

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        AppSettings = services.ConfigureSettingsServices<AppSettings>(Configuration);
        if (AppSettings == null) { throw new InvalidOperationException(); }

        // TODO: retrieve these
        AppSettings.DATABASE.MigrationAssemblyNames.Add(
            DatabaseType.Postgres, "mark.davison.athens.api.migrations.postgres");
        AppSettings.DATABASE.MigrationAssemblyNames.Add(
            DatabaseType.Sqlite, "mark.davison.athens.api.migrations.sqlite");

        services
            .AddLogging()
            .AddAuthorization()
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = AppSettings.AUTH.AUTHORITY;
                o.Audience = AppSettings.AUTH.CLIENT_ID;
            });
        services.ConfigureHealthCheckServices<InitializationHostedService>();
        services.AddScoped<ICurrentUserContext, CurrentUserContext>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
            options.AddPolicy("AllowOrigin", _ => _
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader()
                .Build()
            ));

        services.UseDatabase<AthensDbContext>(AppSettings.PRODUCTION_MODE, AppSettings.DATABASE);
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(AppSettings, new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        }));
        services.AddScoped<IRepository>(_ =>
            new AthensRepository(
                _.GetRequiredService<IDbContextFactory<AthensDbContext>>(),
                _.GetRequiredService<ILogger<AthensRepository>>())
            );

        services.AddScoped<IReadonlyRepository>(_ =>
            new AthensRepository(
                _.GetRequiredService<IDbContextFactory<AthensDbContext>>(),
                _.GetRequiredService<ILogger<AthensRepository>>())
            );

        services.AddSingleton<IDateService>(new DateService(DateService.DateMode.Utc));
        services.UseCQRSServer();
        services
            .AddHttpClient()
            .AddHttpContextAccessor();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("AllowOrigin");

        app.UseHttpsRedirection();

        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<RequestResponseLoggingMiddleware>();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<HydrateAuthenticationFromClaimsPrincipalMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .MapHealthChecks();

            endpoints
                .ConfigureCQRSEndpoints();
        });
    }
}
