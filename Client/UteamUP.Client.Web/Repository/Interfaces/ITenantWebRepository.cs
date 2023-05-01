using UteamUP.Shared.ModelDto;

namespace UteamUP.Client.Repository.Interfaces;

public interface ITenantWebRepository
{
    Task<List<TenantDto>> GetMyTenants(string userOid);
    Task<TenantDto> GetTenantById(int id);
    Task<List<TenantDto>> GetAll();
    Task<List<MUserDto>> GetTenantUsers(int tenantId);
}