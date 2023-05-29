using Blazored.FluentValidation;
using UteamUP.Shared.States;

namespace UteamUP.Client.Web.WizardComponents.WizardBase;

public class WizardBase<TModel> : ComponentBase
{
    [Parameter]
    public int? Id { get; set; }
    

    protected TModel _model;

    
    protected FluentValidationValidator? _fluentValidationValidator;
    protected int _activeStepIndex = 0;
}