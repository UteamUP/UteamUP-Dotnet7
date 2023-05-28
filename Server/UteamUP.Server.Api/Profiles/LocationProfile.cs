namespace UteamUP.Server.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>();
        CreateMap<LocationDto, Location>();
    }
}