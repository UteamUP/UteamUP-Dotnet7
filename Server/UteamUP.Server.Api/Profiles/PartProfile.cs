namespace UteamUP.Server.Api.Profiles;

public class PartProfile : Profile
{
    public PartProfile()
    {
        CreateMap<Part, PartDto>().ReverseMap();
        CreateMap<PartDto, Part>().ReverseMap();
    }
}