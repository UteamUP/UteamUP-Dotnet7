using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Profiles;

public class TenantPlanProfile : Profile
{
    public TenantPlanProfile()
    {
        CreateMap<TenantPlanForm, Plan>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
            .ReverseMap();
    }
}