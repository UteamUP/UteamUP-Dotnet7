namespace UteamUP.Client.Web.WizardComponents.AddEditLocation.Forms;

public class AddEditLocationForm
{
    public LocationBasicForm LocationBasicForm { get; set; }
    public LocationDetailsForm LocationDetailsForm { get; set; }
    public LocationAdditionalForm LocationAdditionalForm { get; set; }

    public AddEditLocationForm()
    {
        LocationBasicForm = new LocationBasicForm();
        LocationDetailsForm = new LocationDetailsForm();
        LocationAdditionalForm = new LocationAdditionalForm();
    }
}