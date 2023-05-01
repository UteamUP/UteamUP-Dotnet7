namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState ReduceSetUserActivateAction(GlobalState state, SetUserActivateAction action)
    {
        return state with { IsActivated = action.IsActivated };
    }
}