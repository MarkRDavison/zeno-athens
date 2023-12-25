namespace mark.davison.athens.shared.queries.test.Scenarios.FetchProjects;

[TestClass]
public class FetchProjectsQueryProcessorTests
{
    private readonly ICurrentUserContext currentUserContext;
    private readonly IRepository repository;
    private readonly FetchProjectsQueryProcessor processor;

    private readonly Guid userId = Guid.NewGuid();

    public FetchProjectsQueryProcessorTests()
    {
        currentUserContext = Substitute.For<ICurrentUserContext>();
        repository = Substitute.For<IRepository>();
        processor = new(repository);

        currentUserContext
            .CurrentUser
            .Returns(new User { Id = userId });
    }

    [TestMethod]
    public async Task ProcessAsync_RetrievesFromRepository()
    {
        List<Project> tasks =
            [
                new Project { Id = Guid.NewGuid(), Name = "#1" },
                new Project { Id = Guid.NewGuid(), Name = "#2" },
                new Project { Id = Guid.NewGuid(), Name = "#3" },
                new Project { Id = Guid.NewGuid(), Name = "#4" }
            ];

        var request = new FetchProjectsQueryRequest();

        repository
            .GetEntitiesAsync(
                Arg.Any<Expression<Func<Project, bool>>>(),
                Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => tasks);

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(tasks.Count, response.Value.Count);
    }

    [TestMethod]
    public async Task ProcessAsync_RetrievesFromRepository_BasedOnUserId()
    {
        List<Project> tasks =
            [
                new Project { Id = Guid.NewGuid(), Name = "#1", UserId = Guid.NewGuid() },
                new Project { Id = Guid.NewGuid(), Name = "#2", UserId = Guid.NewGuid() },
                new Project { Id = Guid.NewGuid(), Name = "#3", UserId = userId },
                new Project { Id = Guid.NewGuid(), Name = "#4", UserId = userId }
            ];

        var request = new FetchProjectsQueryRequest();

        repository
            .GetEntitiesAsync(
                Arg.Any<Expression<Func<Project, bool>>>(),
                Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ =>
            {
                var predicate = _.Arg<Expression<Func<Project, bool>>>();
                var func = predicate.Compile();
                return [.. tasks.Where(func)];
            });

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(2, response.Value.Count);
    }
}
