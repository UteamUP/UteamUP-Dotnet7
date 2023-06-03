using FluentValidation;
using UteamUP.Client.Web.WizardComponents.Template.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.Template.AddEditTemplate.Validators;

public class TemplateBasicValidator : AbstractValidator<TemplateBasicForm>
{
    public TemplateBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}