namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetGlobalStateAction
{
    public GlobalState GlobalState { get; }

    public SetGlobalStateAction(GlobalState globalState)
    {
        GlobalState = globalState;
    }
}
