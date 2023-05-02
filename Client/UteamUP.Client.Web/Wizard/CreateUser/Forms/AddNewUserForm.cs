namespace UteamUP.Client.Wizard.CreateUser.Models;

public class AddNewUserForm
{
    public BasicUserDetailsForm BasicUserDetailsForm { get; set; }
    public AddressUserDetailsForm AddressUserDetailsForm { get; set; }

    public AddNewUserForm()
    {
        BasicUserDetailsForm = new BasicUserDetailsForm();
        AddressUserDetailsForm = new AddressUserDetailsForm();
    }
}