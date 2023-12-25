namespace mark.davison.athens.shared.commands.test.Scenarios.CreateProject;

[TestClass]
public class CreateProjectCommandProcessorTests
{
    private readonly IRepository repository;
    private readonly ICurrentUserContext currentUserContext;
    private readonly IEntityDefaulter<Project> entityDefaulter;
    private readonly CreateProjectCommandProcessor processor;

    public CreateProjectCommandProcessorTests()
    {
        repository = Substitute.For<IRepository>();
        currentUserContext = Substitute.For<ICurrentUserContext>();
        entityDefaulter = Substitute.For<IEntityDefaulter<Project>>();
        processor = new(repository, entityDefaulter);
    }

    [TestMethod]
    public async Task ProcessAsync_WhereParentProjectIdNotSet_CreatesParentlessProject()
    {
        var request = new CreateProjectCommandRequest
        {
            Name = "New project",
            Description = "Some description"
        };

        repository
            .UpsertEntityAsync(
                Arg.Any<Project>(),
                Arg.Any<CancellationToken>())
            .Returns(_ => _.Arg<Project>());

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);

        await entityDefaulter
            .Received(1)
            .DefaultAsync(
                Arg.Any<Project>(),
                Arg.Any<User>());

        await repository
            .Received(1)
            .UpsertEntityAsync(
                Arg.Is<Project>(_ =>
                    _.Name == request.Name &&
                    _.Description == request.Description &&
                    _.ParentProjectId == null),
                Arg.Any<CancellationToken>());
    }

    [TestMethod]
    public async Task ProcessAsync_WhereParentProjectSet_CreatesProjectWithParentId()
    {
        var request = new CreateProjectCommandRequest
        {
            Name = "New project",
            Description = "Some description",
            ParentProjectId = Guid.NewGuid()
        };

        repository
            .UpsertEntityAsync(
                Arg.Any<Project>(),
                Arg.Any<CancellationToken>())
            .Returns(_ => _.Arg<Project>());

        var response = await processor.ProcessAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);

        await entityDefaulter
            .Received(1)
            .DefaultAsync(
                Arg.Any<Project>(),
                Arg.Any<User>());

        await repository
            .Received(1)
            .UpsertEntityAsync(
                Arg.Is<Project>(_ =>
                    _.Name == request.Name &&
                    _.Description == request.Description &&
                    _.ParentProjectId == request.ParentProjectId),
                Arg.Any<CancellationToken>());
    }
}
