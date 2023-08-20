using Fluxor;

namespace UteamUP.Client.Web.StateManagement;

public class StaticState
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Oid { get; set; }

    public class UpdateStaticAction
    {
        public StaticState UpdatedState { get; }

        public UpdateStaticAction(StaticState updatedState)
        {
            UpdatedState = updatedState;
        }
    }
}

public class StaticReducers
{
    public static class StaticReducersClass
    {
        [ReducerMethod]
        public static StaticState ReduceUpdateStaticAction(StaticState state, StaticState.UpdateStaticAction action) =>
            action.UpdatedState;
    }
}