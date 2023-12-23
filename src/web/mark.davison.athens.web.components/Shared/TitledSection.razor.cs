namespace mark.davison.athens.web.components.Shared;
public partial class TitledSection
{
    [Parameter, EditorRequired]
    public required string Title { get; set; }

    [Parameter, EditorRequired]
    public RenderFragment? ChildContent { get; set; }
}
