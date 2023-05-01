namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetUserActivateAction
{
    public bool IsActivated { get; }

    public SetUserActivateAction(bool isActivated)
    {
        IsActivated = IsActivated;
    }
}