using Blazored.LocalStorage;
using UteamUP.Client.Web.Stores.Global.Actions;

namespace UteamUP.Client.Web.Stores.Global.Effects;

public class Effects
{
    private readonly IState<GlobalState> _globalState;
    private readonly ILocalStorageService _localStorageService;
    private readonly ITenantWebRepository _tenantWebRepository;

    public Effects(
        IState<GlobalState> globalState, 
        ITenantWebRepository tenantWebRepository, 
        ILocalStorageService localStorageService)
    {
        _globalState = globalState;
        _tenantWebRepository = tenantWebRepository;
        _localStorageService = localStorageService;
    }

    [EffectMethod]
    public async Task HandleFetchDataAction(SetActiveTenantAction action, IDispatcher dispatcher)
    {
        var tenants = (await _tenantWebRepository.GetMyTenants(_globalState.Value.Oid)).OrderBy(x => x.Name).ToList();

        dispatcher.Dispatch(new SetTenantsAction(tenants));
    }
}