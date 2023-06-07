using FluentValidation;
using UteamUP.Client.Web.WizardComponents.AddEditStock.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditStock.Validators;

public class StockBasicValidator : AbstractValidator<StockBasicForm>
{
    public StockBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}