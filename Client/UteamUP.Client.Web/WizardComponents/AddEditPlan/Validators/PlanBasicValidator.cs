using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditPlan.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditPlan.Validators;

public class PlanBasicValidator : AbstractValidator<PlanBasicForm>
{
    public PlanBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}