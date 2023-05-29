namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ILocationRepository
{
    Task<Location?> CreateLocationAsync(Location location, int tenantId);

    Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId);

    Task<List<Tag>> UpdateTagToLocationAsync(List<Tag> tags, int locationId);
    Task<Location> GetByLocationId(int locationId);

    Task<List<Tag>> GetTagsByLocationId(int locationId);
}