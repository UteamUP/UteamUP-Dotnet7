using UteamUP.Shared.ModelDto;

namespace UteamUP.Client.Repository.Interfaces;

public interface ITenantWebRepository
{
    Task<Tenant?> CreateTenantAsync(TenantDto tenant);
    Task<List<Tenant?>?> GetTenantsForUserAsync(string oid, bool onlyOwned);
    Task<List<Tenant>> GetInvitesAsync(string oid);

    /*
    Task<TenantDto> GetTenantById(int id);
    Task<List<TenantDto>> GetAll();
    Task<List<MUserDto>> GetTenantUsers(int tenantId);
    */
}