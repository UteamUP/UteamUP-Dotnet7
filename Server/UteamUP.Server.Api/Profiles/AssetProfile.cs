namespace UteamUP.Server.Profiles;

public class AssetProfile : Profile
{
    public AssetProfile()
    {
        CreateMap<Asset, AssetDto>();
        CreateMap<AssetDto, Asset>();
    }
}