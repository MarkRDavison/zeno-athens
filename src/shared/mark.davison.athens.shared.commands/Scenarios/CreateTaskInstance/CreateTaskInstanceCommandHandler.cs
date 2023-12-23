namespace mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;

// TODO: A way of registering these so we dont have the handlers if they just delegate to the IProccesor/IValidator
[ExcludeFromCodeCoverage]
public class CreateTaskInstanceCommandHandler : ValidateAndProcessCommandHandler<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>
{
    public CreateTaskInstanceCommandHandler(
        ICommandProcessor<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse> processor,
        ICommandValidator<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse> validator
    ) : base(processor, validator)
    {
    }
}
