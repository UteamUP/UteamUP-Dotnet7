namespace UteamUP.Client.Web.WizardComponents.AddEditPart.Forms;

public class PartForm
{
    public PartBasicForm PartBasicForm { get; set; }
    public PartDetailsForm PartDetailsForm { get; set; }
    
    public PartForm()
    {
        PartBasicForm = new PartBasicForm();
        PartDetailsForm = new PartDetailsForm();
    }
}