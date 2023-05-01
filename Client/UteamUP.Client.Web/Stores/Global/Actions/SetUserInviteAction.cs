namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetUserInviteAction
{
    public bool HasTenantInvites { get; }
    public List<InvitedUserDto> TenantsInvited { get; set; } = new();

    public SetUserInviteAction(bool hasTenantInvites, List<InvitedUserDto> tenantsInvited)
    {
        HasTenantInvites = hasTenantInvites;
        TenantsInvited = tenantsInvited;
    }
}