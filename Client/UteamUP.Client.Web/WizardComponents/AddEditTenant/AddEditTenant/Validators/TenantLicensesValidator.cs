using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Validators;

public class TenantLicensesValidator : AbstractValidator<TenantLicensesForm>
{
    public TenantLicensesValidator()
    {
        RuleFor(x => x.Amount).Empty();

    }
}