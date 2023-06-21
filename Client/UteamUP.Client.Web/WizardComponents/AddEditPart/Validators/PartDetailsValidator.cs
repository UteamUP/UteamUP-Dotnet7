using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditPart.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditPart.Validators;

public class PartDetailsValidator : AbstractValidator<PartDetailsForm>
{
    public PartDetailsValidator()
    {
        RuleFor(x => x.Description).Empty();
        RuleFor(x => x.PartNumber).Empty();
        RuleFor(x => x.ModelNumber).Empty();
        RuleFor(x => x.ReferenceNumber).Empty();
        RuleFor(x => x.SerialNumber).Empty();
    }

}