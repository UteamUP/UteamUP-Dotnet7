namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IAssetWebRepository
{
    Task<Asset> CreateAssetAsync(AssetDto? asset, int tenantId = 0, int vendorId = 0);
}