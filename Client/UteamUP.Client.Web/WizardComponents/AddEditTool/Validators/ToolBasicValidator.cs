using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Validators;

public class ToolBasicValidator : AbstractValidator<ToolBasicForm>
{
    public ToolBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Tags).Empty();
    }
}