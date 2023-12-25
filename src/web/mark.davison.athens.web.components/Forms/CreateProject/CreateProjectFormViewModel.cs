namespace mark.davison.athens.web.components.Forms.CreateProject;

public class CreateProjectFormViewModel : IFormViewModel
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid? ParentProjectId { get; set; }
    public bool Valid => !string.IsNullOrEmpty(Name);
}
