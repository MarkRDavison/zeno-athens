namespace mark.davison.athens.shared.commands.test.Scenarios.CreateTaskInstance;

[TestClass]
public class CreateTaskInstanceCommandProcessorTests
{
    private readonly ICurrentUserContext currentUserContext;
    private readonly IRepository repository;
    private readonly IEntityDefaulter<TaskInstance> entityDefaulter;
    private readonly CreateTaskInstanceCommandProcessor processor;

    public CreateTaskInstanceCommandProcessorTests()
    {
        currentUserContext = Substitute.For<ICurrentUserContext>();
        repository = Substitute.For<IRepository>();
        entityDefaulter = Substitute.For<IEntityDefaulter<TaskInstance>>();
        processor = new(repository, entityDefaulter);
    }

    [TestMethod]
    public async Task ProcessAsync_CreatesNewTaskInstance()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
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

        await repository
            .Received(1)
            .UpsertEntityAsync(
                Arg.Is<TaskInstance>(_ => _.Title == request.Title),
                Arg.Any<CancellationToken>());
    }
}
