using mark.davison.athens.web.features.Project.CreateProject;

namespace mark.davison.athens.web.components.Forms.CreateProject;

public class CreateProjectFormSubmission : IFormSubmission<CreateProjectFormViewModel>
{
    private readonly ICQRSDispatcher _dispatcher;

    public CreateProjectFormSubmission(
        ICQRSDispatcher dispatcher
    )
    {
        _dispatcher = dispatcher;
    }

    public async Task<bool> Primary(CreateProjectFormViewModel formViewModel)
    {
        var request = new CreateProjectFeatureRequest
        {
            Name = formViewModel.Name,
            Description = formViewModel.Description,
            ParentProjectId = formViewModel.ParentProjectId
        };

        var response = await _dispatcher.Dispatch<CreateProjectFeatureRequest, CreateProjectFeatureResponse>(request, CancellationToken.None);

        // TODO: Optional bool/string return variable?? So we can pop a message bar,
        // take some action on the modal
        return response.Success;
    }
}
