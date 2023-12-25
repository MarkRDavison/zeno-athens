namespace mark.davison.athens.shared.utilities.EntityExtensions;

public static class ProjectExtensions
{
    public static ProjectDto ToDto(this Project project)
    {
        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            ParentId = project.ParentProjectId
        };
    }
}
