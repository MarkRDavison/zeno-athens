using mark.davison.athens.shared.models.dtos.Scenarios.Queries.FetchProjects;

namespace mark.davison.athens.api.test.Scenarios.Queries;


[TestClass]
public class FetchProjectsQueryTests : ApiIntegrationTestBase
{
    private readonly List<Project> _existingProjects = new();

    protected override async Task SeedTestData()
    {
        var repository = GetRequiredService<IRepository>();
        await using (repository.BeginTransaction())
        {
            _existingProjects.AddRange(await repository.UpsertEntitiesAsync(new List<Project>
            {
                new(){ Id = Guid.NewGuid(), Name = "Project #1", UserId = CurrentUser.Id },
                new(){ Id = Guid.NewGuid(), Name = "Project #2", UserId = CurrentUser.Id },
                new(){ Id = Guid.NewGuid(), Name = "Project #3", UserId = CurrentUser.Id },
                new(){ Id = Guid.NewGuid(), Name = "Project #4", UserId = AlternateUser.Id }
            }, CancellationToken.None));
        }
    }

    [TestMethod]
    public async Task FetchProjects_RetrievesExistingProjects()
    {
        var request = new FetchProjectsQueryRequest();

        var handler = GetRequiredService<IQueryHandler<FetchProjectsQueryRequest, FetchProjectsQueryResponse>>();
        var currentUserContext = GetRequiredService<ICurrentUserContext>();

        var response = await handler.Handle(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.IsTrue(
            response.Value.Count >= // Default project added by base class
            _existingProjects.Count(_ => _.UserId == CurrentUser.Id));
    }
}
