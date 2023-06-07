namespace UteamUP.Client.Web.WizardComponents.AddEditReport.Forms;

public class ReportForm
{
    public ReportBasicForm ReportBasicForm { get; set; }

    public ReportForm()
    {
        ReportBasicForm = new ReportBasicForm();
    }
}