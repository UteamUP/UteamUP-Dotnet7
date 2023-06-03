using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Validators;

public class TenantAddressValidator : AbstractValidator<TenantAddressForm>
{
    public TenantAddressValidator()
    {
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.PostalCode).NotEmpty();
    }
}