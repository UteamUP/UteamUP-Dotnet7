namespace UteamUP.Client.Web.WizardComponents.AddEditTool.Forms;

public class ToolForm
{
    public ToolBasicForm ToolBasicForm { get; set; }
    public ToolDetailsForm ToolDetailsForm { get; set; }
    public ToolNumberForm ToolNumberForm { get; set; }
    public ToolSizeForm ToolSizeForm { get; set; }
    public ToolPriceForm ToolPriceForm { get; set; }
    

    public ToolForm()
    {
        ToolBasicForm = new ToolBasicForm();
        ToolDetailsForm = new ToolDetailsForm();
        ToolNumberForm = new ToolNumberForm();
        ToolSizeForm = new ToolSizeForm();
        ToolPriceForm = new ToolPriceForm();
    }
}