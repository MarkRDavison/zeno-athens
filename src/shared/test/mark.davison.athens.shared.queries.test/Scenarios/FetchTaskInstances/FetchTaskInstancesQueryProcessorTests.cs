namespace mark.davison.athens.shared.queries.test.Scenarios.FetchTaskInstances;

[TestClass]
public class FetchTaskInstancesQueryProcessorTests
{
    private readonly ICurrentUserContext currentUserContext;
    private readonly IRepository repository;
    private readonly FetchTaskInstancesQueryProcessor processor;

    private readonly Guid userId = Guid.NewGuid();

    public FetchTaskInstancesQueryProcessorTests()
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
        List<TaskInstance> tasks =
            [
                new TaskInstance { Id = Guid.NewGuid(), Title = "#1" },
                new TaskInstance { Id = Guid.NewGuid(), Title = "#2" },
                new TaskInstance { Id = Guid.NewGuid(), Title = "#3" },
                new TaskInstance { Id = Guid.NewGuid(), Title = "#4" }
            ];

        var request = new FetchTaskInstancesQueryRequest();

        repository
            .GetEntitiesAsync(
                Arg.Any<Expression<Func<TaskInstance, bool>>>(),
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
        List<TaskInstance> tasks =
            [
                new TaskInstance { Id = Guid.NewGuid(), Title = "#1", UserId = Guid.NewGuid() },
                new TaskInstance { Id = Guid.NewGuid(), Title = "#2", UserId = Guid.NewGuid() },
                new TaskInstance { Id = Guid.NewGuid(), Title = "#3", UserId = userId },
                new TaskInstance { Id = Guid.NewGuid(), Title = "#4", UserId = userId }
            ];

        var request = new FetchTaskInstancesQueryRequest();

        repository
            .GetEntitiesAsync(
                Arg.Any<Expression<Func<TaskInstance, bool>>>(),
                Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ =>
            {
                var predicate = _.Arg<Expression<Func<TaskInstance, bool>>>();
                var func = predicate.Compile();
                return [.. tasks.Where(func)];
            });

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(2, response.Value.Count);
    }
}
