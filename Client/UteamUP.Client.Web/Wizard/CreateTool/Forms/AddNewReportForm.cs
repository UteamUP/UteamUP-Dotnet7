namespace UteamUP.Client.Wizard.ActivateUser.Forms;

public class AddNewToolForm
{
    public BasicToolForm BasicToolForm { get; set; }
    public ToolDetailsForm ToolDetailsForm { get; set; }
    public ToolUploadForm ToolUploadForm { get; set; }
    public ToolSettingsForm ToolSettingsForm { get; set; }
    public ToolSizesForm ToolSizesForm { get; set; }
    public ToolDescriptionForm ToolDescriptionForm { get; set; }
    public AddNewToolForm()
    {
        BasicToolForm = new BasicToolForm();
        ToolDetailsForm = new ToolDetailsForm();
        ToolUploadForm = new ToolUploadForm();
        ToolSettingsForm = new ToolSettingsForm();
        ToolSizesForm = new ToolSizesForm();
        ToolDescriptionForm = new ToolDescriptionForm();
    }
}