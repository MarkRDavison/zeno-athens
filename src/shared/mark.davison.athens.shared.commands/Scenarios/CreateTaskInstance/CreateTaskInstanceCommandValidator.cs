namespace mark.davison.athens.shared.commands.Scenarios.CreateTaskInstance;

public class CreateTaskInstanceCommandValidator : ICommandValidator<CreateTaskInstanceCommandRequest, CreateTaskInstanceCommandResponse>
{
    private readonly IRepository _repository;
    private readonly ICreateTaskInstanceCache _createTaskInstanceCache;

    public CreateTaskInstanceCommandValidator(
        IRepository repository,
        ICreateTaskInstanceCache createTaskInstanceCache
    )
    {
        _repository = repository;
        _createTaskInstanceCache = createTaskInstanceCache;
    }

    public async Task<CreateTaskInstanceCommandResponse> ValidateAsync(CreateTaskInstanceCommandRequest request, ICurrentUserContext currentUserContext, CancellationToken cancellationToken)
    {
        var response = new CreateTaskInstanceCommandResponse();

        if (string.IsNullOrEmpty(request.Title))
        {
            response.Errors.Add(
                CommonMessages.FormatMessageParameters(
                    CommonMessages.INVALID_PROPERTY,
                    nameof(TaskInstance.Title)));
        }

        await using (_repository.BeginTransaction())
        {
            if (request.ProjectId == null ||
                request.ProjectId == Guid.Empty)
            {
                if (_createTaskInstanceCache.DefaultProjectId == Guid.Empty)
                {
                    var options = await _repository.GetEntityAsync<UserOptions>(
                        _ =>
                            _.UserId == currentUserContext.CurrentUser.Id,
                        cancellationToken);

                    if (options != null &&
                        options.DefaultProjectId != null &&
                        options.DefaultProjectId != Guid.Empty)
                    {
                        _createTaskInstanceCache.DefaultProjectId = options.DefaultProjectId.Value;
                    }
                    else
                    {
                        response.Errors.Add(
                            CommonMessages.FormatMessageParameters(
                                CommonMessages.INVALID_PROPERTY,
                                nameof(TaskInstance.ProjectId),
                                ProjectMessages.MISSING_DEFAULT_PROJECT_ID));
                    }
                }
            }
            else
            {
                var exists = await _repository.EntityExistsAsync<Project>(
                    _ =>
                        _.UserId == currentUserContext.CurrentUser.Id &&
                        _.Id == request.ProjectId,
                    cancellationToken);

                if (!exists)
                {
                    response.Errors.Add(
                        CommonMessages.FormatMessageParameters(
                            CommonMessages.INVALID_PROPERTY,
                            nameof(TaskInstance.ProjectId)));
                }
            }
        }

        return response;
    }
}
