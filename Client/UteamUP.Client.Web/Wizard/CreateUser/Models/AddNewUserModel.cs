namespace UteamUP.Client.Wizard.CreateUser.Models;

public class AddNewUserModel
{
    public BasicUserDetailsStep BasicUserDetailsStep { get; set; } = new();
    public AddressUserDetailsStep AddressUserDetailsStep { get; set; } = new();
}