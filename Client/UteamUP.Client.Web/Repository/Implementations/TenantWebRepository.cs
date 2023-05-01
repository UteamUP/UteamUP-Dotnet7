using UteamUP.Shared.ModelDto;

namespace UteamUP.Client.Repository.Implementations;

public class TenantWebRepository : ITenantWebRepository
{
    public Task<List<TenantDto>> GetMyTenants(string userOid)
    {
        throw new NotImplementedException();
    }

    public Task<TenantDto> GetTenantById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<TenantDto>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<MUserDto>> GetTenantUsers(int tenantId)
    {
        throw new NotImplementedException();
    }
}