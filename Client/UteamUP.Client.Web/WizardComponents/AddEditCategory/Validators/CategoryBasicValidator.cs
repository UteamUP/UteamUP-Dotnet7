using FluentValidation;
using UteamUP.Client.Web.WizardComponents.Template.AddEditTemplate.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditCategory.Validators;

public class CategoryBasicValidator : AbstractValidator<TemplateBasicForm>
{
    public CategoryBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}