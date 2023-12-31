namespace mark.davison.athens.web.features.Tasks.UpdateTaskInstance.UpdateTaskInstanceFavourite;

public class UpdateTaskInstanceFavouriteHandler : ICommandHandler<UpdateTaskInstanceFavouriteRequest, UpdateTaskInstanceFavouriteResponse>
{
    private readonly ICQRSDispatcher _dispatcher;

    public UpdateTaskInstanceFavouriteHandler(ICQRSDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<UpdateTaskInstanceFavouriteResponse> Handle(UpdateTaskInstanceFavouriteRequest command, CancellationToken cancellation)
    {
        var request = new UpdateTaskInstanceFeatureRequest
        {
            TaskInstanceId = command.TaskInstanceId,
            Changes = [
                new DiscriminatedPropertyChangeset
                {
                    // TODO: These names need to match on the dto and the model if the auto apply is to work
                    Name = nameof(TaskInstanceDto.IsFavourite),
                    PropertyType = typeof(bool).FullName!,
                    Value = command.IsFavourite
                }
            ]
        };

        var response = await _dispatcher.Dispatch<UpdateTaskInstanceFeatureRequest, UpdateTaskInstanceFeatureResponse>(request, cancellation);

        return new UpdateTaskInstanceFavouriteResponse
        {
            Errors = [.. response.Errors],
            Warnings = [.. response.Warnings]
        };
    }
}
