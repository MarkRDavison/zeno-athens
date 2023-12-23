namespace mark.davison.athens.shared.utilities.EntityExtensions;

public static class TaskInstanceExtensions
{
    public static TaskInstanceDto ToDto(this TaskInstance taskInstance)
    {
        return new TaskInstanceDto
        {
            Id = taskInstance.Id,
            Title = taskInstance.Title
        };
    }
}
