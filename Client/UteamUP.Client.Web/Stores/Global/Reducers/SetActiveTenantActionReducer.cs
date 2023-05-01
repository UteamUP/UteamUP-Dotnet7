namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState ReduceSetActiveTenantAction(GlobalState state, SetActiveTenantAction action)
    {
        return state with { ActiveTenant = action.Tenant };
    }
}