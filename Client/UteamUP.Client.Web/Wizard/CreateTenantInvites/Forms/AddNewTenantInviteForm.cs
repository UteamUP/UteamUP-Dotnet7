namespace UteamUP.Client.Web.Wizard.CreateTenantInvites.Forms;

public class AddNewTenantInviteForm
{
    public TenantSelectForm TenantSelectForm { get; set; }
    public UserInviteForm UserInviteForm { get; set; }
    public UserInviteListForm UserInviteListForm { get; set; }

    public AddNewTenantInviteForm(TenantSelectForm tenantSelectForm, UserInviteForm userInviteForm, UserInviteListForm userInviteListForm)
    {
        TenantSelectForm = tenantSelectForm;
        UserInviteForm = userInviteForm;
        UserInviteListForm = userInviteListForm;
    }

}