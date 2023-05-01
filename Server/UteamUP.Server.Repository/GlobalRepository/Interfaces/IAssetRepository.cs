namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IAssetRepository
{
    // Get all assets paginated
    Task<IEnumerable<AssetDto>> GetAllAssetsPaginatedAsync(int pageNumber, int pageSize);
}