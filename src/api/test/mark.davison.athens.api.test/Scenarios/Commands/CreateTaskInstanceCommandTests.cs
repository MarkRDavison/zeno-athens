namespace mark.davison.athens.api.test.Scenarios.Commands;

[TestClass]
public class CreateTaskInstanceCommandTests : ApiIntegrationTestBase
{
    [TestMethod]
    public async Task CreateTaskInstance_ForValidTask_Succeeds()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = "Integration test: " + Guid.NewGuid().ToString()
        };

        var handler = GetRequiredService<ICommandHandler<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>>();
        var currentUserContext = GetRequiredService<ICurrentUserContext>();

        var response = await handler.Handle(request, currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
        Assert.IsNotNull(response.Value);
        Assert.AreEqual(request.Title, response.Value.Title);
        Assert.AreNotEqual(Guid.Empty, response.Value.Id);
    }
    [TestMethod]
    public async Task CreateTaskInstance_ForInvalidTask_Fails()
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = string.Empty
        };

        var handler = GetRequiredService<ICommandHandler<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>>();
        var currentUserContext = GetRequiredService<ICurrentUserContext>();

        var response = await handler.Handle(request, currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);
    }
}
