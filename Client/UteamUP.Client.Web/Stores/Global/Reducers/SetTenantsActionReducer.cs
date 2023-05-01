namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState SetTenantsAction(GlobalState state, SetTenantsAction action)
    {
        return state with { Tenants = action.Tenants };
    }
}