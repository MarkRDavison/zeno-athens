using mark.davison.athens.shared.commands.Scenarios.UpdateTaskInstance;
using mark.davison.athens.shared.models.dtos.Scenarios.Commands.UpdateTaskInstance;
using mark.davison.common.Changeset;

namespace mark.davison.athens.shared.commands.test.Scenarios.UpdateTaskInstance;

[TestClass]
public class UpdateTaskInstanceCommandProcessorTests
{
    private readonly ICurrentUserContext _currentUserContext;
    private readonly IRepository _repository;
    private readonly UpdateTaskInstanceCommandProcessor _processor;
    private readonly TaskInstance _existingTask;

    public UpdateTaskInstanceCommandProcessorTests()
    {
        _currentUserContext = Substitute.For<ICurrentUserContext>();
        _repository = Substitute.For<IRepository>();
        _processor = new(_repository);
        _existingTask = new()
        {
            Id = Guid.NewGuid()
        };

        _repository
            .GetEntityAsync<TaskInstance>(
                Arg.Is<Guid>(_ => _ == _existingTask.Id),
                Arg.Any<CancellationToken>())
            .Returns(_existingTask);
    }

    [TestMethod]
    public async Task ProcessAsync_WhereTaskDoesNotExist_ReturnsFailedToFindEntity()
    {
        var request = new UpdateTaskInstanceCommandRequest
        {
            TaskInstanceId = Guid.NewGuid()
        };

        var response = await _processor.ProcessAsync(request, _currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);
        Assert.IsTrue(response.Errors.Any(
            _ =>
                _.Contains(CommonMessages.FAILED_TO_FIND_ENTITY) &&
                _.Contains(nameof(TaskInstance)) &&
                _.Contains(request.TaskInstanceId.ToString())));
    }

    [TestMethod]
    public async Task ProcessAsync_WhereTaskFailsToSave_ReturnsErrorSaving()
    {
        var request = new UpdateTaskInstanceCommandRequest
        {
            TaskInstanceId = _existingTask.Id
        };

        var response = await _processor.ProcessAsync(request, _currentUserContext, CancellationToken.None);

        Assert.IsFalse(response.Success);
        Assert.IsTrue(response.Errors.Any(
            _ =>
                _.Contains(CommonMessages.ERROR_SAVING)));
    }

    [TestMethod]
    public async Task ProcessAsync_WhereChangesProvided_SavesWithChangesetsApplied()
    {
        bool newValue = !_existingTask.IsCompleted;
        var request = new UpdateTaskInstanceCommandRequest
        {
            TaskInstanceId = _existingTask.Id,
            Changes = [
                new DiscriminatedPropertyChangeset
                {
                    Name = nameof(TaskInstance.IsCompleted),
                    PropertyType = typeof(bool).FullName!,
                    Value = newValue
                }
            ]
        };

        _repository
            .UpsertEntityAsync<TaskInstance>(
                Arg.Is<TaskInstance>(_ => _.IsCompleted == newValue),
                Arg.Any<CancellationToken>())
            .Returns(_ => _.Arg<TaskInstance>());

        var response = await _processor.ProcessAsync(request, _currentUserContext, CancellationToken.None);

        Assert.IsTrue(response.Success);
    }
}
