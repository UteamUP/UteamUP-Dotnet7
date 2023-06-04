namespace UteamUP.Server.Api.Profiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagDto>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Name))
            .ForMember(src => src.TenantId, dst => dst.MapFrom(e => e.TenantId));

        CreateMap<TagDto, Tag>()
            .ForMember(dst => dst.Name, src => src.MapFrom(e => e.Name))
            .ForMember(dst => dst.TenantId, src => src.MapFrom(e => e.TenantId));

    }
}
