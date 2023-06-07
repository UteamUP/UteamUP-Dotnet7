using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditCategory.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditCategory.Validators;

public class CategoryBasicValidator : AbstractValidator<CategoryBasicForm>
{
    public CategoryBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}