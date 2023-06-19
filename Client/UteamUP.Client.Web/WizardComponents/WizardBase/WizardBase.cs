using System.Reflection.Metadata;
using Blazored.FluentValidation;
using Blazored.Modal.Services;
using UteamUP.Client.Web.Services;
using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Modals;
using UteamUP.Shared.States;

namespace UteamUP.Client.Web.WizardComponents.WizardBase;

public class WizardBase<TModel> : ComponentBase
{
    [Inject]
    public IModalService ModalService { get; set; }
    public UserState UserState { get; set; }
    public CustomAuthenticationStateProvider CustomAuthStateProvider { get; set; }
    public IMapper Mapper { get; set; }

    [Parameter]
    public int? Id { get; set; }
    //public int TenantId { get; set; } = 0;

    protected TModel _model;

    protected FluentValidationValidator? _fluentValidationValidator;
    protected int _activeStepIndex = 0;
    
    public IDictionary<string, bool> _steps = new Dictionary<string, bool>()
    {
        { "", false },
    };

    public async Task OnClickContinueButtonAsync()
    {
        _steps[_steps.ElementAt(_activeStepIndex).Key] = true;
        _activeStepIndex = _activeStepIndex + 1;
    }

    public void OnClickBackButton()
    {
        _activeStepIndex = _activeStepIndex - 1;
    }
    
    public void OpenModal(string ErrorTitle, string ErrorDetails)
    {
        var options = new ModalOptions()
        {
            DisableBackgroundCancel = true,
            HideCloseButton = false,
        };
        
        var parameters = new ModalParameters();
        parameters.Add(ErrorTitle, ErrorDetails);

        ModalService.Show<ErrorTenantModal>("ErrorDetails", parameters, options);
    }
}