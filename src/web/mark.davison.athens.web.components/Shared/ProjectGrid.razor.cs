namespace mark.davison.athens.web.components.Shared;

public partial class ProjectGrid
{
    [Parameter, EditorRequired]
    public required IEnumerable<ProjectDto> Projects { get; set; }
}
