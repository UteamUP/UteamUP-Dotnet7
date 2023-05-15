namespace UteamUP.Client.Wizard.ActivateUser.Forms;

public class AddNewStockForm
{
    public BasicStockForm BasicStockForm { get; set; }
    public StockDetailsForm StockDetailsForm { get; set; }
    public StockAssignmentForm StockAssignmentForm { get; set; }

    public AddNewStockForm()
    {
        BasicStockForm = new BasicStockForm();
        StockDetailsForm = new StockDetailsForm();
        StockAssignmentForm = new StockAssignmentForm();
    }
}