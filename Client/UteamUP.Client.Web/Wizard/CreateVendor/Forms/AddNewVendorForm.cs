namespace UteamUP.Client.Web.Wizard.CreateVendor.Forms;

public class AddNewVendorForm
{
    public BasicVendorInfoForm BasicVendorInfoForm { get; set; }
    public TimeStateForm TimeStateForm { get; set; }
    public VendorDetailsForm VendorDetailsForm { get; set; }

    public AddNewVendorForm()
    {
        BasicVendorInfoForm = new BasicVendorInfoForm();
        TimeStateForm = new TimeStateForm();
        VendorDetailsForm = new VendorDetailsForm();
    }
}