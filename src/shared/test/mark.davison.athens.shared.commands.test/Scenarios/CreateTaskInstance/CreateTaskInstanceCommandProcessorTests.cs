namespace mark.davison.athens.shared.commands.test.Scenarios.CreateTaskInstance;

[TestClass]
public class CreateTaskInstanceCommandProcessorTests
{
    private readonly ICurrentUserContext currentUserContext;
    private readonly IRepository repository;
    private readonly IEntityDefaulter<TaskInstance> entityDefaulter;
    private readonly ICreateTaskInstanceCache createTaskInstanceCache;
    private readonly CreateTaskInstanceCommandProcessor processor;

    public CreateTaskInstanceCommandProcessorTests()
    {
        currentUserContext = Substitute.For<ICurrentUserContext>();
        repository = Substitute.For<IRepository>();
        entityDefaulter = Substitute.For<IEntityDefaulter<TaskInstance>>();
        createTaskInstanceCache = Substitute.For<ICreateTaskInstanceCache>();
        processor = new(repository, entityDefaulter, createTaskInstanceCache);
    }

    [TestMethod]
    public async Task ProcessAsync_CreatesNewTaskInstance_ForSpecifiedProjectId()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            ProjectId = Guid.NewGuid(),
            Title = "A new task"
        };

        repository
            .UpsertEntityAsync(
                Arg.Any<TaskInstance>(),
                Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => _.Arg<TaskInstance>());

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(request.Title, response.Value.Title);
        Assert.AreEqual(request.ProjectId, response.Value.ProjectId);

        await repository
            .Received(1)
            .UpsertEntityAsync(
                Arg.Any<TaskInstance>(),
                Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task ProcessAsync_CreatesNewTaskInstance_ForDefaultProjectId()
    {
        var defaultProjectId = Guid.NewGuid();
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = "A new task"
        };

        createTaskInstanceCache
            .DefaultProjectId
            .Returns(defaultProjectId);

        repository
            .UpsertEntityAsync(
                Arg.Any<TaskInstance>(),
                Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(_ => _.Arg<TaskInstance>());

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(request.Title, response.Value.Title);
        Assert.AreEqual(defaultProjectId, response.Value.ProjectId);

        await repository
            .Received(1)
            .UpsertEntityAsync(
                Arg.Any<TaskInstance>(),
                Arg.Any<CancellationToken>());
    }
}
