using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditLocation.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditLocation.Validators;

public class LocationAdditionalValidator : AbstractValidator<LocationAdditionalForm>
{
    public LocationAdditionalValidator()
    {
        RuleFor(x => x.Tags).Empty();
    }
}