using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditReport.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditReport.Validators;

public class ReportBasicValidator : AbstractValidator<ReportBasicForm>
{
    public ReportBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}