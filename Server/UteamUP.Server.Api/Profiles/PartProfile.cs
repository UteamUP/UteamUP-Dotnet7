namespace UteamUP.Server.Profiles;

public class PartProfile : Profile
{
    public PartProfile()
    {
        CreateMap<Part, PartDto>();
        CreateMap<PartDto, Part>();
    }
}