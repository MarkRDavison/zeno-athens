namespace mark.davison.athens.shared.commands.test.Scenarios.CreateTaskInstance;

[TestClass]
public class CreateTaskInstanceCommandValidatorTests
{
    private readonly ICurrentUserContext currentUserContext;
    private readonly CreateTaskInstanceCommandValidator validator;

    public CreateTaskInstanceCommandValidatorTests()
    {
        currentUserContext = Substitute.For<ICurrentUserContext>();
        validator = new();
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
    public async Task ValidateAsync_PassesWhenTaskIsValid()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = "Some task name"
        };

        var response = await validator.ValidateAsync(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
    }
}
