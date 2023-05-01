namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IPartRepository
{
    // Get parts by asset id paginated
    Task<IEnumerable<Part>> GetAssetPartByAssetIdPaginatedAsync(int assetId, int pageNumber, int pageSize);

    // Get all parts paginated
    Task<IEnumerable<Part>> GetAllPartsPaginatedAsync(int pageNumber, int pageSize);

    // Get part by id
    Task<Part> GetPartByIdAsync(int id);
}