namespace UteamUP.Client.Web.Wizard.CreateUser.Forms;

public class AddNewUserForm
{
    public BasicUserDetailsForm BasicUserDetailsForm { get; set; }
    public AddressUserDetailsForm AddressUserDetailsForm { get; set; }
    public LicenseAgreementForm LicenseAgreementForm { get; set; }

    public AddNewUserForm()
    {
        BasicUserDetailsForm = new BasicUserDetailsForm();
        AddressUserDetailsForm = new AddressUserDetailsForm();
        LicenseAgreementForm = new LicenseAgreementForm();
    }
}