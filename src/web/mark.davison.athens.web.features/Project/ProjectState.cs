namespace mark.davison.athens.web.features.Project;

public class ProjectState : IState
{
    public ProjectState() : this(Enumerable.Empty<ProjectDto>())
    {
    }

    public ProjectState(IEnumerable<ProjectDto> accounts)
    {
        Projects = accounts.ToList();
        LastModified = DateTime.Now;
    }

    public void Initialise()
    {
        Projects = Enumerable.Empty<ProjectDto>();
        LastModified = default;
    }

    public IEnumerable<ProjectDto> Projects { get; private set; }
    public DateTime LastModified { get; private set; }

}
