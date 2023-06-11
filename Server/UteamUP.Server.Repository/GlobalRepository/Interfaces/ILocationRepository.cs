namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ILocationRepository
{
    Task<LocationTagDto?> GetByLocationId(int locationId);
    Task<List<Location>> GetLocationsByTenantId(int tenantId);

    Task<Location> UpdateLocationWithTags(Location location, List<string> tags, int id);

    Task<Location?> CreateLocationWithTags(Location? location, List<string> tags);
}