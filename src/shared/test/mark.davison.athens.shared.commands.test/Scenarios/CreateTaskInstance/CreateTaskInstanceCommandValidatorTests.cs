namespace mark.davison.athens.shared.commands.test.Scenarios.CreateTaskInstance;

[TestClass]
public class CreateTaskInstanceCommandValidatorTests
{
    private readonly IRepository repository;
    private readonly ICreateTaskInstanceCache createTaskInstanceCache;
    private readonly ICurrentUserContext currentUserContext;
    private readonly CreateTaskInstanceCommandValidator validator;

    public CreateTaskInstanceCommandValidatorTests()
    {
        repository = Substitute.For<IRepository>();
        createTaskInstanceCache = Substitute.For<ICreateTaskInstanceCache>();
        currentUserContext = Substitute.For<ICurrentUserContext>();
        validator = new(repository, createTaskInstanceCache);
    }

    [TestMethod]
    public async Task ValidateAsync_FailsWhenTitleIsNullOrEmpty()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = string.Empty
        };

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);

        Assert.IsTrue(response.Errors.Any(
            _ => _.StartsWith(CommonMessages.INVALID_PROPERTY)));
    }

    [TestMethod]
    public async Task ValidateAsync_FailsWhenProjectId_NotValid()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            ProjectId = Guid.NewGuid(),
            Title = string.Empty
        };

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);

        Assert.IsTrue(response.Errors.Any(
            _ =>
                _.StartsWith(CommonMessages.INVALID_PROPERTY) &&
                _.Contains(nameof(TaskInstance.ProjectId))));
    }

    [TestMethod]
    public async Task ValidateAsync_FailsWhenProjectId_NotSet_AndDefaultProjectIdNotSet()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = string.Empty
        };

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);

        Assert.IsTrue(response.Errors.Any(
            _ =>
                _.StartsWith(CommonMessages.INVALID_PROPERTY) &&
                _.Contains(nameof(TaskInstance.ProjectId)) &&
                _.Contains(ProjectMessages.MISSING_DEFAULT_PROJECT_ID)));
    }

    [TestMethod]
    public async Task ValidateAsync_PassesWhenTaskIsValid_ForProvidedProjectId()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            ProjectId = Guid.NewGuid(),
            Title = "Some task name"
        };

        repository
            .EntityExistsAsync<Project>(
                Arg.Any<Expression<Func<Project, bool>>>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(true);

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
    }

    [TestMethod]
    public async Task ValidateAsync_PassesWhenTaskIsValid_ForDefaultProjectId()
    {
        var currentUserId = Guid.NewGuid();
        var defaultProjectId = Guid.NewGuid();
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = "Some task name"
        };

        currentUserContext
            .CurrentUser
            .Returns(new User { Id = currentUserId });

        repository
            .GetEntityAsync<UserOptions>(
                Arg.Any<Expression<Func<UserOptions, bool>>>(),
                Arg.Any<CancellationToken>()
            )
            .Returns(_ =>
            {
                var expression = _.Arg<Expression<Func<UserOptions, bool>>>();

                var options = new UserOptions
                {
                    UserId = currentUserId,
                    DefaultProjectId = defaultProjectId
                };

                if (expression.Compile()(options))
                {
                    return options;
                }

                return null;
            });

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
    }
}
