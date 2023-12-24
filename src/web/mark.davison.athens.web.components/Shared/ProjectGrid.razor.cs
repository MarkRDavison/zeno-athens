namespace mark.davison.athens.web.components.Shared;

public partial class ProjectGrid
{
    [Parameter, EditorRequired]
    public required List<ProjectDto> Projects { get; set; }
}
