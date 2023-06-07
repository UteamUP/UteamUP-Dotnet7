using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Validators;

public class ToolDetailsValidator : AbstractValidator<ToolDetailsForm>
{
    public ToolDetailsValidator()
    {
        RuleFor(x => x.Notes).Empty();
        RuleFor(x => x.Description).Empty();
        RuleFor(x => x.AdditionalInfo).Empty();
        RuleFor(x => x.ImageUrl).Empty();
    }
}