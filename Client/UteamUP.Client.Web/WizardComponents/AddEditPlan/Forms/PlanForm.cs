namespace UteamUP.Client.Web.WizardComponents.AddEditPlan.Forms;

public class PlanForm
{
    public PlanBasicForm PlanBasicForm { get; set; }

    public PlanForm()
    {
        PlanBasicForm = new PlanBasicForm();
    }
}