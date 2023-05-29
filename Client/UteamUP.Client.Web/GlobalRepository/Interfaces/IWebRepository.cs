namespace UteamUP.Client.GlobalRepository.Interfaces;

public interface IWebRepository<T>
{
    Task<IEnumerable<T>> GetAll(string url);
    Task<IEnumerable<T>> GetAllByTenantId(string url, int tenantId);
    Task<T> Get(int id, string url);
    Task<T> GetByName(string name, string url);
    Task<T> GetByNameAndTenantId(string name, string url, int tenantId);
    Task<T> Add(T entity, string url);
    Task<T> AddByTenantId(T entity, string url, int tenantId);
    Task<T> Update(T entity, string url);
    Task<T> UpdateByTenantId(T entity, string url, int tenantId);

    Task<T> Delete(T entity, string url);
    Task<T> DeleteByTenantId(T entity, string url, int tenantId);
}