namespace UteamUP.Server.Api.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>().ReverseMap();
        CreateMap<LocationDto, Location>().ReverseMap();
    }
}