namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetActiveTenantAction
{
    public Tenant Tenant { get; }

    public SetActiveTenantAction(Tenant tenant)
    {
        Tenant = tenant;
    }
}