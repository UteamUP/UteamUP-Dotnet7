using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Validators;

public class TenantPlanValidator : AbstractValidator<TenantPlanForm>
{
    public TenantPlanValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}