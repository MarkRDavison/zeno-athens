namespace mark.davison.athens.web.components.Forms.CreateProject;

public class CreateProjectModalViewModel : IModalViewModel<CreateProjectFormViewModel, CreateProjectForm>
{
    private readonly IFormSubmission<CreateProjectFormViewModel> _formSubmission;

    public CreateProjectModalViewModel(IFormSubmission<CreateProjectFormViewModel> formSubmission)
    {
        _formSubmission = formSubmission;
    }

    public CreateProjectFormViewModel FormViewModel { get; set; } = new();

    public Task<bool> Primary(CreateProjectFormViewModel formViewModel) => _formSubmission.Primary(formViewModel);
}
