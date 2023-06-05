namespace UteamUP.Server.Api.Profiles;

public class LocationTagProfile : Profile
{
    public LocationTagProfile()
    {
        CreateMap<LocationTag, TagDto>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Tag.Name))
            .ForMember(src => src.TenantId, dst => dst.MapFrom(e => e.Tag.TenantId))
            .ReverseMap()
            .ForPath(dst => dst.Tag, opt => opt.MapFrom(src => src.Name))
            .ForPath(dst => dst.Tag.TenantId, opt => opt.MapFrom(src => src.TenantId));

        CreateMap<LocationDto, LocationTagDto>()
            .ForMember(src => src.Tags, dst => dst.MapFrom(e => e.Tags))
            .ForMember(src => src.TenantId, dst => dst.MapFrom(e => e.TenantId))
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Name))
            .ForMember(src => src.Description, dst => dst.MapFrom(e => e.Description))
            .ReverseMap();
    }
}