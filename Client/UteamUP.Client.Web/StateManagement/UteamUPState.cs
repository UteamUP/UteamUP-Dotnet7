// File: UteamUPState.cs

using Fluxor;

namespace UteamUP.Client.Web.StateManagement;

public class UteamUPStateClass
{
    public StaticState StaticState { get; }
    public DynamicState<object> DynamicState { get; }

    public UteamUPStateClass(StaticState staticState, DynamicState<object> dynamicState)
    {
        StaticState = staticState;
        DynamicState = dynamicState;
    }
}

public class UteamUPFeature : Feature<UteamUPStateClass>
{
    public override string GetName() => "UteamUP";

    protected override UteamUPStateClass GetInitialState() =>
        new UteamUPStateClass(
            new StaticState(),
            new DynamicState<object>()
        );
}