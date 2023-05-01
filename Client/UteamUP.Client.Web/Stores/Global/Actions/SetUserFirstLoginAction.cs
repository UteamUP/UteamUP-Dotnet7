namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetUserFirstLoginAction
{
    public bool FirstLogin { get; }

    public SetUserFirstLoginAction(bool firstlogin)
    {
        FirstLogin = firstlogin;
    }
}