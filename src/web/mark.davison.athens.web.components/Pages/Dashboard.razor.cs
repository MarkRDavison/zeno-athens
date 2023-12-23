using Microsoft.AspNetCore.Components.Web;

namespace mark.davison.athens.web.components.Pages;

public partial class Dashboard
{
    public List<object> RecentlyViewedProjects { get; } = new();
    public List<string> CurrentTasks { get; } = new();

    public async Task AddTaskInstance()
    {
        if (!string.IsNullOrEmpty(NewTaskName))
        {
            CurrentTasks.Add(NewTaskName);
            NewTaskName = string.Empty;
            await Task.CompletedTask;
        }
    }

    public string NewTaskName { get; set; } = string.Empty;
    public async void OnKeyDown(KeyboardEventArgs args)
    {
        if (args.Key == "Enter")
        {
            await AddTaskInstance();
        }
    }
}
