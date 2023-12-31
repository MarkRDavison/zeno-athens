﻿namespace mark.davison.athens.shared.models.dtos.Scenarios.Commands.CreateTaskInstance;

[PostRequest(Path = "create-task-command")]
public class CreateTaskInstanceCommandRequest : ICommand<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>
{
    public Guid? ProjectId { get; set; }
    public string Title { get; set; } = string.Empty;
}
