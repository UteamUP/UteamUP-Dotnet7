namespace UteamUP.Client.Web.WizardComponents.AddEditVendor.Forms;

public class VendorForm
{
    public VendorBasicForm VendorBasicForm { get; set; }

    public VendorForm()
    {
        VendorBasicForm = new VendorBasicForm();
    }
}