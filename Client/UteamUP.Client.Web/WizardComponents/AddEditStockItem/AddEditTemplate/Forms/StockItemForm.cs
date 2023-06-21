using UteamUP.Client.Web.WizardComponents.Template.AddEditTemplate.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditStockItem.AddEditTemplate.Forms;

public class StockItemForm
{
    public StockItemBasicForm StockItemBasicForm { get; set; }

    public StockItemForm()
    {
        StockItemBasicForm = new StockItemBasicForm();
    }
}