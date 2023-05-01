namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetTenantsAction
{
    public List<TenantDto> Tenants { get; set; } = new();

    public SetTenantsAction(List<TenantDto> tenants)
    {
        Tenants = tenants;
    }
}