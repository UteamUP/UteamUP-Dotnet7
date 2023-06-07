namespace UteamUP.Client.Web.WizardComponents.AddEditWorkorder.Forms;

public class WorkorderForm
{
    public WorkorderBasicForm WorkorderBasicForm { get; set; }

    public WorkorderForm()
    {
        WorkorderBasicForm = new WorkorderBasicForm();
    }
}