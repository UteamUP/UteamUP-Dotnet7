namespace UteamUP.Server.Api.Profiles;

public class LocationTagProfile : Profile
{
    public LocationTagProfile()
    {
        CreateMap<LocationTag, TagDto>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Tag.Name))
            .ForMember(src => src.TenantId, dst => dst.MapFrom(e => e.Tag.TenantId))
            .ReverseMap()
            .ForPath(dst => dst.Tag.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dst => dst.Tag.TenantId, opt => opt.MapFrom(src => src.TenantId));
    }
}