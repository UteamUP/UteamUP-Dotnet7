using UteamUP.Shared.ModelDto;

namespace UteamUP.Client.Repository.Interfaces;

public interface ITenantWebRepository
{
    Task<Tenant?> CreateTenantAsync(TenantDto tenant, int planId, int extraLicenses);
    Task<List<Tenant>> GetOwnedTenantsAsync(string oid);
    Task<List<Tenant>> GetAllTenantsByOidAsync(string oid);
    Task<Tenant> GetTenantById(string tenantId);
    Task<List<Tenant>> GetInvitesAsync(string oid);

    /*
    Task<TenantDto> GetTenantById(int id);
    Task<List<TenantDto>> GetAll();
    Task<List<MUserDto>> GetTenantUsers(int tenantId);
    */
}