namespace UteamUP.Client.Web.Stores.Global.Reducers;

public class SetGlobalStateActionReducer
{
    [ReducerMethod]
    public static GlobalState ReduceSetGlobalStateAction(GlobalState state, SetGlobalStateAction action)
    {
        return action.GlobalState;
    }
}