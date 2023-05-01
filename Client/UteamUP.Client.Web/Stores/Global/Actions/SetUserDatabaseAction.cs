namespace UteamUP.Client.Web.Stores.Global.Actions;

public class SetUserDatabaseAction
{
    public bool HasDatabaseUser { get; }

    public SetUserDatabaseAction(bool hasDatabaseUser)
    {
        HasDatabaseUser = hasDatabaseUser;
    }
}