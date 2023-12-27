namespace mark.davison.athens.web.components.Pages;

public partial class Dashboard
{
    private bool _stateLoading;
    public List<object> RecentlyViewedProjects { get; } = new();

    private IStateInstance<TaskInstanceListState> TaskInstanceListState { get; set; } = default!;

    protected override void OnInitialized()
    {
        TaskInstanceListState = GetState<TaskInstanceListState>();
    }

    protected override async Task OnParametersSetAsync()
    {
        await EnsureStateLoaded();
    }

    private async Task EnsureStateLoaded()
    {
        _stateLoading = true;
        await Dispatcher.Dispatch(
            new FetchTaskInstanceListStateAction(true),
            CancellationToken.None
        );
        _stateLoading = false;
    }

    public async Task CreateTaskInstance()
    {
        if (!string.IsNullOrEmpty(QuickAddText))
        {
            // TODO: Have knowledge of whether current user has a default project set up
            // if not, then dont allow tasks to be submitted without specifying a project
            var quickAddText = QuickAddText;
            QuickAddText = string.Empty;

            var response = await _dispatcher.Dispatch<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>(new CreateTaskInstanceFeatureRequest
            {
                TaskCreateInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(quickAddText)
            }, CancellationToken.None); ;

            if (!response.Success)
            {
                Console.Error.WriteLine("FAILED, TODO: TOAST: {0}", string.Join(",", response.Errors));
            }

        }
    }

    public string QuickAddText { get; set; } = string.Empty;
    public async void OnKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            await CreateTaskInstance();
        }
    }
}
