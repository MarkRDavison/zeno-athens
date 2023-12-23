using mark.davison.athens.shared.models.dtos.Shared;
using mark.davison.athens.web.features.Task.FetchTaskInstances;

namespace mark.davison.athens.web.components.Pages;

public partial class Dashboard
{

    public List<object> RecentlyViewedProjects { get; } = new();
    public List<TaskInstanceDto> CurrentTasks { get; } = new();

    protected override async Task OnInitializedAsync()
    {
        var response = await _dispatcher.Dispatch<FetchTaskInstancesFeatureRequest, FetchTaskInstancesFeatureResponse>(CancellationToken.None);

        if (!response.Success)
        {
            Console.Error.WriteLine("FAILED, TODO: TOAST: {0}", string.Join(",", response.Errors));
        }

        if (response.Value != null)
        {
            CurrentTasks.AddRange(response.Value);
        }
    }

    public async Task AddTaskInstance()
    {
        if (!string.IsNullOrEmpty(NewTaskName))
        {
            var title = NewTaskName;
            NewTaskName = string.Empty;
            CurrentTasks.Add(new TaskInstanceDto
            {
                Title = title
            });

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
