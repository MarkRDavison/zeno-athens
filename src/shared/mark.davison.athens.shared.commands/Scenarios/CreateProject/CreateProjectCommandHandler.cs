namespace mark.davison.athens.shared.commands.Scenarios.CreateProject;

public class CreateProjectCommandHandler : ValidateAndProcessCommandHandler<CreateProjectCommandRequest, CreateProjectCommandResponse>
{
    public CreateProjectCommandHandler(
        ICommandProcessor<CreateProjectCommandRequest, CreateProjectCommandResponse> processor,
        ICommandValidator<CreateProjectCommandRequest, CreateProjectCommandResponse> validator
    ) : base(
        processor,
        validator
    )
    {
    }
}
