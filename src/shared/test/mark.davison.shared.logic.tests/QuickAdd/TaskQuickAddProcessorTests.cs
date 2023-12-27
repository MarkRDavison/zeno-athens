namespace mark.davison.shared.logic.tests.QuickAdd;

[TestClass]
public class TaskQuickAddProcessorTests
{
    [TestMethod]
    public void ResolveTaskQuickAddCommand_WithEmptyString_ReturnsInvalid()
    {
        var command = string.Empty;

        var createInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(command);

        Assert.IsFalse(createInfo.Valid);
    }

    [TestMethod]
    public void ResolveTaskQuickAddCommand_WithSingleWordString_ReturnsValid_WithExpectedTaskName()
    {
        var taskName = "Dishes";
        var command = $"{taskName}";

        var createInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(command);

        Assert.IsTrue(createInfo.Valid);
        Assert.AreEqual(taskName, createInfo.Name);
    }


    [TestMethod]
    public void ResolveTaskQuickAddCommand_WithMultiWordString_ReturnsValid_WithExpectedTaskName()
    {
        var taskName = "Do the dishes";
        var command = $"{taskName}";

        var createInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(command);

        Assert.IsTrue(createInfo.Valid);
        Assert.AreEqual(taskName, createInfo.Name);
    }

    [TestMethod]
    public void ResolveTaskQuickAddCommand_WithSingleWordTaskAndProject_ReturnsValid_WithExpectedNames()
    {
        var taskName = "Dishes";
        var projectName = "Housework";
        var command = $"{taskName} {TaskQuickAddProcessor.ProjectIdentifier}{projectName}";

        var createInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(command);

        Assert.IsTrue(createInfo.Valid);
        Assert.AreEqual(taskName, createInfo.Name);
        Assert.AreEqual(projectName, createInfo.ProjectName);
    }

    [TestMethod]
    public void ResolveTaskQuickAddCommand_WithMultiWordTaskAndProject_ReturnsValid_WithExpectedNames()
    {
        var taskName = "Do the dishes";
        var projectName = "Housework makes things good";
        var command = $"{taskName} {TaskQuickAddProcessor.ProjectIdentifier}{projectName}";

        var createInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(command);

        Assert.IsTrue(createInfo.Valid);
        Assert.AreEqual(taskName, createInfo.Name);
        Assert.AreEqual(projectName, createInfo.ProjectName);
    }
}