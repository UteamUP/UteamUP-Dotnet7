
namespace UteamUP.Client.Web.Stores.Global.Actions;


public class SetUserAction
{
    public string Oid { get; }
    public string Name { get; }
    public string Email { get; }

    public SetUserAction(string oid, string name, string email)
    {
        Oid = oid;
        Name = name;
        Email = email;
    }
}