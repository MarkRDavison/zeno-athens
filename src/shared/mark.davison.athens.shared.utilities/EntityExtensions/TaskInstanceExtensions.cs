﻿namespace mark.davison.athens.shared.utilities.EntityExtensions;

public static class TaskInstanceExtensions
{
    public static TaskInstanceDto ToDto(this TaskInstance taskInstance)
    {
        return new TaskInstanceDto
        {
            Id = taskInstance.Id,
            ProjectId = taskInstance.ProjectId,
            Title = taskInstance.Title,
            DueTime = taskInstance.DueTime,
            IsCompleted = taskInstance.IsCompleted,
            IsFavourite = taskInstance.IsFavourite
        };
    }
}
