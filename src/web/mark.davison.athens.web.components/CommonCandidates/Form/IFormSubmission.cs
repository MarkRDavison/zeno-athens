namespace mark.davison.athens.web.components.CommonCandidates.Form;

public interface IFormSubmission<TFormViewModel> where TFormViewModel : IFormViewModel
{
    Task<bool> Primary(TFormViewModel formViewModel);
}
