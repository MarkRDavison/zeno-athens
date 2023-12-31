namespace mark.davison.athens.web.features.Tasks.UpdateTaskInstance.UpdateTaskInstanceFavourite;

public class UpdateTaskInstanceFavouriteRequest : ICommand<UpdateTaskInstanceFavouriteRequest, UpdateTaskInstanceFavouriteResponse>
{
    public Guid TaskInstanceId { get; set; }
    public bool IsFavourite { get; set; }
}
