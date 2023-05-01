namespace UteamUP.Client.Web.Stores.Global.Reducers;

public static partial class Reducers
{
    [ReducerMethod]
    public static GlobalState ReduceSetUserAction(GlobalState state, SetUserAction action)
    {
        Console.WriteLine("Old State: Name - " + state.Name + ", Oid - " + state.Oid + ", Email - " + state.Email);
        Console.WriteLine("Action: Name - " + action.Name + ", Oid - " + action.Oid + ", Email - " + action.Email);

        var newState = state with { Name = action.Name, Oid = action.Oid, Email = action.Email };

        Console.WriteLine("New Sftate: Name - " + newState.Name + ", Oid - " + newState.Oid + ", Email - " +
                          newState.Email);

        return newState;
    }
}