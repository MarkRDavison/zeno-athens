namespace mark.davison.athens.web.features.Tasks.FetchTaskInstanceListState;

public class FetchTaskInstanceListateStateActionHandler : IActionHandler<FetchTaskInstanceListStateAction>
{
    private readonly IStateStore _stateStore;
    private readonly ICQRSDispatcher _dispatcher;

    public FetchTaskInstanceListateStateActionHandler(
        IStateStore stateStore,
        ICQRSDispatcher dispatcher
    )
    {
        _stateStore = stateStore;
        _dispatcher = dispatcher;
    }

    public async Task Handle(FetchTaskInstanceListStateAction action, CancellationToken cancellation)
    {
        var request = new FetchTaskInstancesFeatureRequest
        {
            ShowCompleted = action.ShowCompleted
        };

        var response = await _dispatcher.Dispatch<FetchTaskInstancesFeatureRequest, FetchTaskInstancesFeatureResponse>(request, cancellation);

        if (response.Success && response.Value != null)
        {
            _stateStore.SetState(new TaskInstanceListState(response.Value));
        }
        else
        {
            Console.Error.WriteLine("FetchTaskInstancesFeature: Failed, TODO: TOAST");
        }
    }
}
