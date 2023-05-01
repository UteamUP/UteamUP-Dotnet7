namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState SetUserFirstLoginAction(GlobalState state, SetUserFirstLoginAction action)
    {
        return state with { FirstLogin = action.FirstLogin };
    }
}