namespace mark.davison.athens.web.components.Pages;

public partial class Dashboard
{

    public List<object> RecentlyViewedProjects { get; } = new();
    public List<string> CurrentTasks { get; } = new();

    public async Task AddTaskInstance()
    {
        if (!string.IsNullOrEmpty(NewTaskName))
        {
            var title = NewTaskName;
            NewTaskName = string.Empty;
            CurrentTasks.Add(title);

            var response = await _dispatcher.Dispatch<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>(new CreateTaskInstanceFeatureRequest
            {
                Title = title
            }, CancellationToken.None);

            if (!response.Success)
            {
                Console.Error.WriteLine("FAILED, TODO: TOAST: {0}", string.Join(",", response.Errors));
            }

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
