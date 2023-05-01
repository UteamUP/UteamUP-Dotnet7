namespace UteamUP.Client.Web.Base;

public abstract class UteamupComponentBase : Fluxor.Blazor.Web.Components.FluxorComponent
{
    [Inject] public IState<GlobalState>? GlobalState { get; set; }

    [Inject] public IActionSubscriber? ActionSubscriber { get; set; }
}