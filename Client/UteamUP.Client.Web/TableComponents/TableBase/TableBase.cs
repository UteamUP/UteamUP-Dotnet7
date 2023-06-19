using Blazored.Modal.Services;
using UteamUP.Client.Web.Services;
using UteamUP.Shared.States;

namespace UteamUP.Client.Web.TableComponents.TableBase;

public class TableBase<TModel> : ComponentBase
{
    [Inject]
    public IModalService ModalService { get; set; }
    public UserState UserState { get; set; }
    public CustomAuthenticationStateProvider CustomAuthStateProvider { get; set; }
    public IMapper Mapper { get; set; }

    [Parameter]
    public int? Id { get; set; }

    protected TModel _model;
}