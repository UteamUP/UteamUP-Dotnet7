using UteamUP.Client.Web.WizardComponents.AddEditVendor.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditVendor.Profiles;

public class VendorFormProfile : Profile
{
    public VendorFormProfile()
    {
        CreateMap<VendorDto, VendorForm>().ReverseMap();
        CreateMap<VendorForm, VendorDto>().ReverseMap();
        CreateMap<VendorBasicForm, VendorDto>()
            .ForMember(src => src.Name, act => act.MapFrom(dst => dst.Name))
            .ForMember(src => src.Email, act => act.MapFrom(dst => dst.Email))
            .ForMember(src => src.PhoneNumber, act => act.MapFrom(dst => dst.PhoneNumber))
            .ForMember(src => src.WebSite, act => act.MapFrom(dst => dst.WebSite))
            .ForMember(src => src.Description, act => act.MapFrom(dst => dst.Description))
            .ReverseMap();
        CreateMap<VendorDto, VendorBasicForm>().ReverseMap();
        CreateMap<Vendor, VendorBasicForm>()
            .ForMember(src => src.Name, act => act.MapFrom(dst => dst.Name))
            .ForMember(src => src.Email, act => act.MapFrom(dst => dst.Email))
            .ForMember(src => src.PhoneNumber, act => act.MapFrom(dst => dst.PhoneNumber))
            .ForMember(src => src.WebSite, act => act.MapFrom(dst => dst.WebSite))
            .ForMember(src => src.Description, act => act.MapFrom(dst => dst.Description))
            .ReverseMap();
    }
}