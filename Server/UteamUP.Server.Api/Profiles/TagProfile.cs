namespace UteamUP.Server.Api.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Name))
            .ForMember(src => src.TenantId, dst => dst.MapFrom(e => e.TenantId))
            .ReverseMap();
        CreateMap<TagDto, Tag>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Name))
            .ForMember(src => src.TenantId, dst => dst.MapFrom(e => e.TenantId))
            .ReverseMap();
    }
}