namespace mark.davison.athens.web.features.Tasks;

public class TaskInstanceListState : IState
{
    public TaskInstanceListState() : this(Enumerable.Empty<TaskInstanceDto>())
    {
    }

    public TaskInstanceListState(IEnumerable<TaskInstanceDto> accounts)
    {
        TaskInstances = accounts.ToList();
        LastModified = DateTime.Now;
    }

    public void Initialise()
    {
        TaskInstances = Enumerable.Empty<TaskInstanceDto>();
        LastModified = default;
    }

    public IEnumerable<TaskInstanceDto> TaskInstances { get; private set; }
    public DateTime LastModified { get; private set; }

}
