namespace mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;

public class CreateTaskInstanceCommandValidator : ICommandValidator<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>
{
    public Task<CreateTaskInstanceCommandResponse> ValidateAsync(CreateTaskInstanceCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var response = new CreateTaskInstanceCommandResponse();

        if (string.IsNullOrEmpty(request.Title))
        {
            response.Errors.Add(
                CommonMessages.FormatMessageParameters(
                    CommonMessages.INVALID_PROPERTY,
                    nameof(TaskInstance.Title)));
        }

        return Task.FromResult(response);
    }
}
