using FluentValidation;
using UteamUP.Client.Web.WizardComponents.Template.AddEditTemplate.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditStockItem.AddEditTemplate.Validators;

public class StockItemBasicValidator : AbstractValidator<StockItemBasicForm>
{
    public StockItemBasicValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}