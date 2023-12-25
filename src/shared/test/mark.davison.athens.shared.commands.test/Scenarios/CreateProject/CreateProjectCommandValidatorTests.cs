namespace mark.davison.athens.shared.commands.test.Scenarios.CreateProject;

[TestClass]
public class CreateProjectCommandValidatorTests
{
    private readonly IRepository repository;
    private readonly ICurrentUserContext currentUserContext;
    private readonly CreateProjectCommandValidator validator;

    public CreateProjectCommandValidatorTests()
    {
        repository = Substitute.For<IRepository>();
        currentUserContext = Substitute.For<ICurrentUserContext>();
        validator = new(repository);
    }

    [TestMethod]
    public async Task ValidateAsync_WhereNoParentProjectIdSet_DoesNotCallRepository()
    {
        var request = new CreateProjectCommandRequest
        {
            Name = "New project",
            Description = "Some description"
        };

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsFalse(repository.ReceivedCalls().Any());
    }

    [TestMethod]
    public async Task ValidateAsync_WhereParentProjectIdSetAndExists_Succeeds()
    {
        var request = new CreateProjectCommandRequest
        {
            Name = "New project",
            Description = "Some description",
            ParentProjectId = Guid.NewGuid()
        };

        repository
            .EntityExistsAsync<Project>(
                Arg.Any<Guid>())
            .Returns(true);

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);

        await repository
            .Received(1)
            .EntityExistsAsync<Project>(
                Arg.Is<Guid>(_ => _ == request.ParentProjectId));
    }

    [TestMethod]
    public async Task ValidateAsync_WhereParentProjectIdSetAndDoesNotExist_Fails()
    {
        var request = new CreateProjectCommandRequest
        {
            Name = "New project",
            Description = "Some description",
            ParentProjectId = Guid.NewGuid()
        };

        repository
            .EntityExistsAsync<Project>(
                Arg.Any<Guid>())
            .Returns(false);

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);
        Assert.IsTrue(response.Errors.Contains(
            CommonMessages.FormatMessageParameters(
                CommonMessages.INVALID_PROPERTY,
                nameof(Project.ParentProjectId))));

        await repository
            .Received(1)
            .EntityExistsAsync<Project>(
                Arg.Is<Guid>(_ => _ == request.ParentProjectId));
    }
}
