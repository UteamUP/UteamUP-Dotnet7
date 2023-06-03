using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;
using UteamUP.Client.Web.WizardComponents.Template.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Validators;

public class TenantBasicValidator : AbstractValidator<TenantBasicForm>
{
    public TenantBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).Empty();
        RuleFor(x => x.ContactEmail).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Website).Empty();
    }
}