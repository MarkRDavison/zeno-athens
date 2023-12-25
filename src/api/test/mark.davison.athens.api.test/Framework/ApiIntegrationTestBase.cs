namespace mark.davison.athens.api.test.Framework;

public class ApiIntegrationTestBase : IntegrationTestBase<AthensApiWebApplicationFactory, AppSettings>
{
    private IServiceScope? _serviceScope;
    public ApiIntegrationTestBase()
    {
        _serviceScope = Services.CreateScope();
        _factory.ModifyCurrentUserContext = (serviceProvider, currentUserContext) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>();
            currentUserContext.CurrentUser = CurrentUser;
            currentUserContext.Token = MockJwtTokens.GenerateJwtToken(new[]
            {
                new Claim("sub", CurrentUser.Sub.ToString()),
                new Claim("aud", appSettings.Value.AUTH.CLIENT_ID)
            });
        };
    }

    protected override async Task SeedData(IServiceProvider serviceProvider)
    {
        await base.SeedData(serviceProvider);
        using var scope = Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<IRepository>();
        await using (repository.BeginTransaction())
        {
            await repository.UpsertEntitiesAsync(
                [CurrentUser, AlternateUser],
                CancellationToken.None);

            await repository.UpsertEntitiesAsync(
                [CurrentUserDefaultProject, AlternateUserDefaultProject],
                CancellationToken.None);

            await repository.UpsertEntitiesAsync(
                [
                    new UserOptions
                    {
                        Id = Guid.NewGuid(),
                        UserId = CurrentUser.Id,
                        DefaultProjectId = CurrentUserDefaultProject.Id
                    },
                    new UserOptions
                    {
                        Id = Guid.NewGuid(),
                        UserId = AlternateUser.Id,
                        DefaultProjectId = AlternateUserDefaultProject.Id
                    }
                ],
                CancellationToken.None);
        }
        await SeedTestData();
    }

    protected virtual Task SeedTestData() => Task.CompletedTask;

    protected User CurrentUser { get; } = new User
    {
        Id = Guid.NewGuid(),
        Sub = Guid.NewGuid(),
        Username = "integration.test",
        First = "integration",
        Last = "test",
        Email = "integration.test@gmail.com"
    };
    protected User AlternateUser { get; } = new User
    {
        Id = Guid.NewGuid(),
        Sub = Guid.NewGuid(),
        Username = "alternate.test",
        First = "alternate",
        Last = "test",
        Email = "alternate.test@gmail.com"
    };

    protected Project CurrentUserDefaultProject => new Project
    {
        Id = new Guid("2713C572-C768-4448-9039-040F2C304230"),
        Name = "Default",
        UserId = CurrentUser.Id
    };
    protected Project AlternateUserDefaultProject => new Project
    {
        Id = new Guid("7A3B52F3-4CA1-4A29-9C3A-88CE50634845"),
        Name = "Default",
        UserId = AlternateUser.Id
    };

    protected T GetRequiredService<T>() where T : notnull
    {
        if (_serviceScope == null)
        {
            throw new NullReferenceException();
        }
        return _serviceScope!.ServiceProvider.GetRequiredService<T>();
    }
}
