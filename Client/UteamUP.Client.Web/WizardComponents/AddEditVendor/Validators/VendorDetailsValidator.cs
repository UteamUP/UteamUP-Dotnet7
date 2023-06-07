using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditVendor.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditVendor.Validators;

public class VendorDetailsValidator : AbstractValidator<VendorDetailsForm>
{
    public VendorDetailsValidator()
    {
        RuleFor(x => x.Description).NotEmpty();
    }
}