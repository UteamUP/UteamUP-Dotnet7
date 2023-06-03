using UteamUP.Client.Web.WizardComponents.AddEditLocation.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditLocation.Profiles;

public class AddEditLocationProfile : Profile
{
    public AddEditLocationProfile()
    {
        CreateMap<AddEditLocationForm, Location>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.LocationBasicForm.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.LocationBasicForm.Name))
            .ForMember(dest => dest.TenantId, act => act.MapFrom(src => src.LocationBasicForm.TenantId))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.LocationDetailsForm.Description))
            .ReverseMap();
    }
}