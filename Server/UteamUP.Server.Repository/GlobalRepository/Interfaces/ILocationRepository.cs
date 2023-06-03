namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ILocationRepository
{
    Task<Location?> CreateLocationAsync(Location location, int tenantId);
    Task<Location?> UpdateLocationAsync(Location location);
    Task<Location?> UpdateTagToLocationAsync(int locationId, List<Tag> tags);
    Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId);
    //Task<List<Tag>> UpdateTagToLocationAsync(List<Tag> tags, int locationId);
    Task<Location?> GetByLocationId(int locationId);

    //Task<List<Tag>> GetTagsByLocationId(int locationId);

    Task<Location> UpdateLocationWithTags(int locationId, List<Tag> newTags);

    Task<Location> CreateLocationWithTags(Location location, List<Tag> tags);

    Task DeleteLocation(int locationId);

    Task<Location> GetLocationWithTags(int locationId);
}