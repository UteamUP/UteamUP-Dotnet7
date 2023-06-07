using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Validators;

public class ToolPriceValidator : AbstractValidator<ToolPriceForm>
{
    public ToolPriceValidator()
    {
        RuleFor(x => x.AvgPrice).Empty();
        RuleFor(x => x.MaxPrice).Empty();
        RuleFor(x => x.MinPrice).Empty();
    }
}