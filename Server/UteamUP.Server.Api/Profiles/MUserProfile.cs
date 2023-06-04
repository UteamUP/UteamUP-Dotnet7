namespace UteamUP.Server.Api.Profiles;

public class MUserProfile : Profile
{
    public MUserProfile()
    {
        CreateMap<MUser, MUserDto>()
            .ForMember(a => a.Oid, opt => opt.MapFrom(b => b.Oid))
            .ForMember(a => a.Name, opt => opt.MapFrom(b => b.Name))
            .ForMember(a => a.Email, opt => opt.MapFrom(b => b.Email));
        CreateMap<MUserDto, MUser>()
            .ForMember(a => a.Oid, opt => opt.MapFrom(b => b.Oid))
            .ForMember(a => a.Name, opt => opt.MapFrom(b => b.Name))
            .ForMember(a => a.Email, opt => opt.MapFrom(b => b.Email));
    }
}