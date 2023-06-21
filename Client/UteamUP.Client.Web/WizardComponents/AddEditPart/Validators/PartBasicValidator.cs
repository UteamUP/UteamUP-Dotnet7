using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditPart.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditPart.Validators;

public class PartBasicValidator : AbstractValidator<PartBasicForm>
{
    public PartBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Category).NotEmpty();
        RuleFor(x => x.Vendor).NotEmpty();
    }
}