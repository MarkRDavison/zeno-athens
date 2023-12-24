using mark.davison.athens.shared.models.dtos.Shared;

namespace mark.davison.athens.web.components.Pages;

public partial class Projects
{
    public List<ProjectDto> LoadedProjects { get; } = new();

    protected override Task OnInitializedAsync()
    {
        LoadedProjects.AddRange(
            [
                new ProjectDto { Id = Guid.NewGuid(), Name = "Project #1" },
                new ProjectDto { Id = Guid.NewGuid(), Name = "Project #2" },
                new ProjectDto { Id = Guid.NewGuid(), Name = "Project #3" },
                new ProjectDto { Id = Guid.NewGuid(), Name = "Project #4" },
                new ProjectDto { Id = Guid.NewGuid(), Name = "Project #5" },
                new ProjectDto { Id = Guid.NewGuid(), Name = "Project #6 but some really long text that will overflow the container that it is in" },
            ]);
        return base.OnInitializedAsync();
    }
}
