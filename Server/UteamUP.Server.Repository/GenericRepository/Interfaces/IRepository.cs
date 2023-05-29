namespace UteamUP.Server.Repository.GenericRepository.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAllByTenantId(int tenantId);
    Task<T> Get(int id);
    Task<IEnumerable<T>> GetByName(string name);
    Task<T?> GetByNameAndTenantId(string name, int tenantId);
    Task<T> Add(T entity);
    Task<T> AddByTenantId(T entity, int tenantId);
    Task<T> Update(T entity);
    Task<T> UpdateByTenantId(T entity, int tenantId);
    Task<T> Delete(T entity);
    Task<T> DeleteByTenantId(T entity, int tenantId);
}