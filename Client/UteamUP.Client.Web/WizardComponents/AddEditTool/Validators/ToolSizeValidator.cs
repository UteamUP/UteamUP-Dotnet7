using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Validators;

public class ToolSizeValidator : AbstractValidator<ToolSizeForm>
{
    public ToolSizeValidator()
    {
        RuleFor(x => x.Width).Empty();
        RuleFor(x => x.Height).Empty();
        RuleFor(x => x.Length).Empty();
        RuleFor(x => x.Depth).Empty();
        RuleFor(x => x.Weight).Empty();
        RuleFor(x => x.Value).Empty();
    }
}