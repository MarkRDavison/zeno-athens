namespace mark.davison.athens.shared.commands.Scenarios.CreateProject;

public class CreateProjectCommandValidator : ICommandValidator<CreateProjectCommandRequest, CreateProjectCommandResponse>
{
    private readonly IRepository _repository;

    public CreateProjectCommandValidator(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateProjectCommandResponse> ValidateAsync(CreateProjectCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var response = new CreateProjectCommandResponse();

        if (request.ParentProjectId != null)
        {
            await using (_repository.BeginTransaction())
            {
                if (!await _repository.EntityExistsAsync<Project>(request.ParentProjectId.Value, cancellationToken))
                {
                    response.Errors.Add(
                        CommonMessages.FormatMessageParameters(
                            CommonMessages.INVALID_PROPERTY,
                            nameof(Project.ParentProjectId)));
                }
            }
        }

        return response;
    }
}
