namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ILocationRepository
{
    Task<LocationTagDto?> GetByLocationId(int locationId);

    Task<Location> UpdateLocationWithTags(Location location, List<string> tags, int id);

    Task<Location?> CreateLocationWithTags(Location? location, List<string> tags);
}