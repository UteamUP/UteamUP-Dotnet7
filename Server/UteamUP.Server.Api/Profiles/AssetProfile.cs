namespace UteamUP.Server.Api.Profiles;

public class AssetProfile : Profile
{
    public AssetProfile()
    {
        CreateMap<Asset, AssetDto>().ReverseMap();
    }
}