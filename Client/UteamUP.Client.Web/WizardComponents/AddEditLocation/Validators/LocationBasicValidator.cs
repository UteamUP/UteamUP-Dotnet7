using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditLocation.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditLocation.Validators;

public class LocationValidator : AbstractValidator<LocationBasicForm>
{
    public LocationValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}