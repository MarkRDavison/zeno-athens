namespace mark.davison.athens.web.features.Tasks.FetchTaskInstanceListState;

public class FetchTaskInstanceListStateAction : IAction<FetchTaskInstanceListStateAction>
{
    public FetchTaskInstanceListStateAction(bool showCompleted)
    {
        ShowCompleted = showCompleted;
    }

    public bool ShowCompleted { get; }
}
