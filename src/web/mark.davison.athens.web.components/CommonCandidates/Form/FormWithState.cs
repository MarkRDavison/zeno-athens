namespace mark.davison.athens.web.components.CommonCandidates.Form;

public class FormWithState<TFormViewModel> : ComponentWithState, IForm<TFormViewModel> where TFormViewModel : IFormViewModel
{
    [Parameter, EditorRequired]
    public required TFormViewModel FormViewModel { get; set; }
}
