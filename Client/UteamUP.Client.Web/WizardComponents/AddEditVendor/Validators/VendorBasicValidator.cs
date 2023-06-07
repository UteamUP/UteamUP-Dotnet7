using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditVendor.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditVendor.Validators;

public class VendorBasicValidator : AbstractValidator<VendorBasicForm>
{
    public VendorBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.WebSite).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.PhoneNumber).Empty();
        RuleFor(x => x.Description).Empty();
    }
}