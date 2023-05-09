using UteamUP.Client.Wizard.ActivateUser.Forms;

namespace UteamUP.Client.Web.Wizard.CreatePart.Forms;

public class AddNewPartForm
{
    public BasicPartForm BasicPartForm { get; set; }
    public PartDetailsForm PartDetailsForm { get; set; }
    public PartUploadForm PartUploadForm { get; set; }
    public PartDescriptionForm PartDescriptionForm { get; set; }

    public AddNewPartForm()
    {
        BasicPartForm = new BasicPartForm();
        PartDetailsForm = new PartDetailsForm();
        PartUploadForm = new PartUploadForm();
        PartDescriptionForm = new PartDescriptionForm();
    }
}