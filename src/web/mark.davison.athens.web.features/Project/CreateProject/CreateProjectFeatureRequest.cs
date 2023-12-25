namespace mark.davison.athens.web.features.Project.CreateProject;

public class CreateProjectFeatureRequest : ICommand<CreateProjectFeatureRequest, CreateProjectFeatureResponse>
{
    // These fields are duplicated, can we get a CreateProjectDto or something like that???
    // Or just the ProjectDto, and use the fields you want
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentProjectId { get; set; }
}
