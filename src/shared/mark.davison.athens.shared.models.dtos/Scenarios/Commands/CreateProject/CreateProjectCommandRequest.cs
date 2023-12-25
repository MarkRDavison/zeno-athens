namespace mark.davison.athens.shared.models.dtos.Scenarios.Commands.CreateProject;

[PostRequest(Path = "create-project-command")]
public class CreateProjectCommandRequest : ICommand<CreateProjectCommandRequest, CreateProjectCommandResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentProjectId { get; set; }
}
