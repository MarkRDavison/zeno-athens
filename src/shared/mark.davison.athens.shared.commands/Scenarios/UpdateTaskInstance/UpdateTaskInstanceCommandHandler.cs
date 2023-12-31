namespace mark.davison.athens.shared.commands.Scenarios.UpdateTaskInstance;

// TODO: A way of registering these so we dont have the handlers if they just delegate to the IProccesor/IValidator
[ExcludeFromCodeCoverage]
public class UpdateTaskInstanceCommandHandler : ValidateAndProcessCommandHandler<UpdateTaskInstanceCommandRequest, UpdateTaskInstanceCommandResponse>
{
    public UpdateTaskInstanceCommandHandler(
        ICommandProcessor<UpdateTaskInstanceCommandRequest, UpdateTaskInstanceCommandResponse> processor
    ) : base(processor)
    {
    }
}
