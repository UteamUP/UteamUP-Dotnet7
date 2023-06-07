using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditWorkorder.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditWorkorder.Validators;

public class WorkorderBasicValidator : AbstractValidator<WorkorderBasicForm>
{
    public WorkorderBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}