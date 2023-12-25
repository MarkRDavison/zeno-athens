namespace mark.davison.athens.web.features.Tasks.CreateTaskInstance;

// TODO: The naming of this isn't great, either needs to be merged with the other create one, or renamed
public class CreateTaskInstanceFeatureHandler : ICommandHandler<CreateTaskInstanceFeatureRequest, CreateTaskInstanceFeatureResponse>
{
    private readonly IClientHttpRepository _repository;

    public CreateTaskInstanceFeatureHandler(
        IClientHttpRepository repository
    )
    {
        _repository = repository;
    }
    public async Task<CreateTaskInstanceFeatureResponse> Handle(CreateTaskInstanceFeatureRequest command, CancellationToken cancellation)
    {
        var request = new CreateTaskInstanceCommandRequest
        {
            Title = command.Title
        };

        var response = await _repository.Post<CreateTaskInstanceCommandResponse, CreateTaskInstanceCommandRequest>(request, cancellation);

        return new CreateTaskInstanceFeatureResponse
        {
            Value = response.Value,
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
