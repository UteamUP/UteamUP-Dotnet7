using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditLocation.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditLocation.Validators;

public class LocationDetailsValidator : AbstractValidator<LocationDetailsForm>
{
    public LocationDetailsValidator()
    {
        RuleFor(x => x.Description).Empty();
    }
}