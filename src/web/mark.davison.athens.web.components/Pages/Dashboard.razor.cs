namespace mark.davison.athens.web.components.Pages;

public partial class Dashboard
{
    private bool _stateLoading;
    public List<object> RecentlyViewedProjects { get; } = new();

    private IStateInstance<TaskInstanceListState> TaskInstanceListState { get; set; } = default!;
    private IStateInstance<ProjectState> ProjectState { get; set; } = default!;

    protected override void OnInitialized()
    {
        TaskInstanceListState = GetState<TaskInstanceListState>();
        ProjectState = GetState<ProjectState>();
    }

    protected override async Task OnParametersSetAsync()
    {
        await EnsureStateLoaded();
    }

    private async Task EnsureStateLoaded()
    {
        _stateLoading = true;

        await Task.WhenAll(
            Dispatcher.Dispatch<FetchTaskInstanceListStateAction>(
                new FetchTaskInstanceListStateAction(true),
                CancellationToken.None
            ),
            Dispatcher.Dispatch<FetchProjectsFeatureRequest, FetchProjectsFeatureResponse>(
                new FetchProjectsFeatureRequest(),
                CancellationToken.None
            )
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

            var taskCreateInfo = TaskQuickAddProcessor.ResolveTaskQuickAddCommand(quickAddText);

            if (!taskCreateInfo.Valid)
            {
                Console.Error.WriteLine("FAILED, TODO: TOAST: {0}", "QuickCreateTask not valid");
                return;
            }

            if (!string.IsNullOrEmpty(taskCreateInfo.ProjectName))
            {
                taskCreateInfo.ProjectId = FindProjectByName(taskCreateInfo.ProjectName);
                if (taskCreateInfo.ProjectId == null)
                {
                    Console.Error.WriteLine("FAILED, TODO: Error beneath input: {0}", $"Could not find project by name '{taskCreateInfo.ProjectName}'");
                    return;
                }
            }

            var response = await _dispatcher.Dispatch<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>(new CreateTaskInstanceFeatureRequest
            {
                TaskCreateInfo = taskCreateInfo
            }, CancellationToken.None); ;

            if (!response.Success)
            {
                Console.Error.WriteLine("FAILED, TODO: TOAST: {0}", string.Join(",", response.Errors));
            }

        }
    }

    private Guid? FindProjectByName(string projectName)
    {
        return ProjectState.Instance.Projects.Where(_ => projectName.Equals(_.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault()?.Id;
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
