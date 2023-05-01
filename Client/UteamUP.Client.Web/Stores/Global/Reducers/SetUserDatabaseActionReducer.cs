namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState ReduceSetUserDatabaseAction(GlobalState state, SetUserDatabaseAction action)
    {
        return state with { HasDatabaseUser = action.HasDatabaseUser };
    }
}