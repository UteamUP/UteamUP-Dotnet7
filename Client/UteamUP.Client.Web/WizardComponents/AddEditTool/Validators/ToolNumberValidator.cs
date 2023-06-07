using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Validators;

public class ToolNumberValidator : AbstractValidator<ToolNumberForm>
{
    public ToolNumberValidator()
    {
        RuleFor(x => x.BarcodeNumber).Empty();
        RuleFor(x => x.SerialNumber).Empty();
        RuleFor(x => x.ReferenceNumber).Empty();
        RuleFor(x => x.ModelNumber).Empty();
        RuleFor(x => x.ToolNumber).Empty();
    }
}