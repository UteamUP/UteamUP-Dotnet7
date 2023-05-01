namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState ReduceSetUserInviteAction(GlobalState state, SetUserInviteAction action)
    {
        return state with { HasTenantInvites = action.HasTenantInvites, TenantsInvited = action.TenantsInvited };
    }
}