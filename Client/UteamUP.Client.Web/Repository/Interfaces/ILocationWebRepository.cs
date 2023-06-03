namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ILocationWebRepository
{
    //Task<List<Location>> GetAllLocationsByTenantIdAsync(int tenantId);
    //Task<Location> GetByLocationId(int locationId);
    Task<Location?> Create(LocationDto location);
    //Task<Location?> Update(Location location);
    //Task<List<Tag>> GetTagsByLocationId(int locationId);
    //Task<Location?> UpdateTagToLocationAsync(List<Tag> tags, int locationId);
}