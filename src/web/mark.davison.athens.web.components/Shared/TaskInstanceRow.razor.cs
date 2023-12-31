namespace mark.davison.athens.web.components.Shared;

public partial class TaskInstanceRow
{
    [Parameter, EditorRequired]
    public required TaskInstanceDto Instance { get; set; }

    [Parameter, EditorRequired]
    public required ProjectDto? ProjectInstance { get; set; }

    [Parameter, EditorRequired]
    public required Func<Task> ToggleFavouriteStatus { get; set; }
}
