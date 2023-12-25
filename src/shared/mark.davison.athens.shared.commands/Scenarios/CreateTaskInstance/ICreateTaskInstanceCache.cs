namespace mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;

// TODO: Child Interface ICQRSCache, this allows auto registering when we add it for ICommandProcessor/Validator???
public interface ICreateTaskInstanceCache
{
    Guid DefaultProjectId { get; set; }
}
