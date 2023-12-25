namespace mark.davison.athens.web.components.Pages;

public partial class Projects
{
    private IStateInstance<ProjectState> ProjectState { get; set; } = default!;

    protected override void OnInitialized()
    {
        ProjectState = GetState<ProjectState>();
    }

    protected override async Task OnParametersSetAsync()
    {
        await EnsureStateLoaded();
    }
    private async Task EnsureStateLoaded()
    {
        await Dispatcher.Dispatch<FetchProjectsFeatureRequest, FetchProjectsFeatureResponse>(CancellationToken.None);
    }

    private async Task OpenCreateProjectModal()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = true
        };
        var param = new DialogParameters<Modal<CreateProjectModalViewModel, CreateProjectFormViewModel, CreateProjectForm>>
        {
            { _ => _.PrimaryText, "Save" },
            { _ => _.Instance, null }
        };
        var dialog = _dialogService.Show<Modal<CreateProjectModalViewModel, CreateProjectFormViewModel, CreateProjectForm>>("Create Project", param, options);
        await dialog.Result;
    }
}
