namespace UteamUP.Client.Web.Wizard.CreateVendor.Forms;

public class AddNewVendorForm
{
    public BasicVendorInfoForm BasicVendorInfoForm { get; set; }
    public VendorDetailsForm VendorDetailsForm { get; set; }

    public AddNewVendorForm()
    {
        BasicVendorInfoForm = new BasicVendorInfoForm();
        VendorDetailsForm = new VendorDetailsForm();
    }
}