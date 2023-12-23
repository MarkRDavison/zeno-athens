namespace mark.davison.athens.api.test.Scenarios.Queries;

[TestClass]
public class FetchTaskInstancesQueryTests : ApiIntegrationTestBase
{
    private readonly List<TaskInstance> _existingTasks = new();

    protected override async Task SeedTestData()
    {
        var repository = GetRequiredService<IRepository>();
        await using (repository.BeginTransaction())
        {
            _existingTasks.AddRange(await repository.UpsertEntitiesAsync(new List<TaskInstance>
            {
                new(){ Id = Guid.NewGuid(), Title = "#1", UserId = CurrentUser.Id },
                new(){ Id = Guid.NewGuid(), Title = "#2", UserId = CurrentUser.Id },
                new(){ Id = Guid.NewGuid(), Title = "#3", UserId = CurrentUser.Id },
                new(){ Id = Guid.NewGuid(), Title = "#4", UserId = AlternateUser.Id }
            }, CancellationToken.None));
        }
    }

    [TestMethod]
    public async Task FetchTaskInstances_RetrievesExistingTasks()
    {
        var request = new FetchTaskInstancesQueryRequest();

        var handler = GetRequiredService<IQueryHandler<FetchTaskInstancesQueryRequest, FetchTaskInstancesQueryResponse>>();
        var currentUserContext = GetRequiredService<ICurrentUserContext>();

        var response = await handler.Handle(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(_existingTasks.Count(_ => _.UserId == CurrentUser.Id), response.Value.Count);
    }
}
