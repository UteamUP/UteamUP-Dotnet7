using Fluxor;

namespace UteamUP.Client.Web.StateManagement;

public class DynamicState<T>
{
    public T StateData { get; set; }

    public UpdateDynamicAction<T> UpdateDynamic() =>
        new UpdateDynamicAction<T>();
}

public class UpdateDynamicAction<T>
{
    public DynamicState<T> UpdatedState { get; }

    public UpdateDynamicAction()
    {
        UpdatedState = new DynamicState<T>();
    }
}

public static class DynamicReducers<T>
{
    [ReducerMethod]
    public static DynamicState<T> ReduceUpdateDynamicAction(DynamicState<T> state, UpdateDynamicAction<T> action) =>
        action.UpdatedState;
}