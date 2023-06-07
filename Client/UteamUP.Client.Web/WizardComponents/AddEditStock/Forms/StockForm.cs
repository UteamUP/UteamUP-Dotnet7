namespace UteamUP.Client.Web.WizardComponents.AddEditStock.Forms;

public class StockForm
{
    public StockBasicForm StockBasicForm { get; set; }

    public StockForm()
    {
        StockBasicForm = new StockBasicForm();
    }
}