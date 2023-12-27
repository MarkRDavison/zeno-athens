namespace mark.davison.athens.web.features.Tasks.CreateNewTaskInstance;

public class CreateNewTaskInstanceActionHandler : IActionHandler<CreateNewTaskInstanceAction>
{
    private readonly IStateStore _stateStore;
    private readonly ICQRSDispatcher _dispatcher;

    public CreateNewTaskInstanceActionHandler(
        IStateStore stateStore,
        ICQRSDispatcher dispatcher
    )
    {
        _stateStore = stateStore;
        _dispatcher = dispatcher;
    }

    public async Task Handle(CreateNewTaskInstanceAction action, CancellationToken cancellation)
    {
        var request = new CreateTaskInstanceFeatureRequest
        {
            TaskCreateInfo = action.TaskCreateInfo
        };

        var response = await _dispatcher.Dispatch<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>(request, cancellation);

        if (response.Success && response.Value != null)
        {
            var existing = _stateStore.GetState<TaskInstanceListState>().Instance.TaskInstances;
            _stateStore.SetState(new TaskInstanceListState([.. existing, response.Value]));
        }
        else
        {
            Console.Error.WriteLine("CreateTaskInstanceFeatureRequest: Failed, TODO: TOAST");
        }
    }
}
