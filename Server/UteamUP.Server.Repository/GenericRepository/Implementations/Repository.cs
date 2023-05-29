using UteamUP.Server.Repository.GenericRepository.Interfaces;

namespace UteamUP.Server.Repository.GenericRepository.Implementations;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly pgContext _context;
    private readonly IMapper _mapper;
    private readonly DbSet<T> _dbSet;
    
    public Repository(
        IMapper mapper, 
        pgContext context
        )
    {
        _mapper = mapper;
        _context = context;
        _dbSet = _context.Set<T>();
    }


    public async Task<IEnumerable<T>> GetAll()
    {
        return _dbSet;
    }
    
    public async Task<IEnumerable<T>> GetAllByTenantId(int tenantId)
    {
        var tenantIdProperty = typeof(T).GetProperty("TenantId");
        if (tenantIdProperty != null && tenantIdProperty.PropertyType == typeof(int))
        {
            return await _dbSet.Where(e => (int)tenantIdProperty.GetValue(e) == tenantId).ToListAsync();
        }
        else
        {
            throw new InvalidOperationException($"The {typeof(T).Name} class does not have a TenantId property of type int.");
        }
    }

    public async Task<T> Get(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public async Task<IEnumerable<T>> GetByName(string name)
    {
        // Check if the T class has a property named "Name"
        var property = typeof(T).GetProperty("Name");
        if (property != null && property.PropertyType == typeof(string))
        {
            // If it does, use it to filter the entities
            return await _dbSet.Where(e => (string)property.GetValue(e) == name).ToListAsync();
        }
        else
        {
            // If it doesn't, throw an exception (or handle this however you like)
            throw new InvalidOperationException($"The {typeof(T).Name} class does not have a Name property of type string.");
        }
    }
    
    public async Task<T?> GetByNameAndTenantId(string name, int tenantId)
    {
        // Check if the T class has a property named "Name" and "TenantId"
        var nameProperty = typeof(T).GetProperty("Name");
        var tenantIdProperty = typeof(T).GetProperty("TenantId");
        if (nameProperty != null && nameProperty.PropertyType == typeof(string) &&
            tenantIdProperty != null && tenantIdProperty.PropertyType == typeof(int))
        {
            // If it does, use it to filter the entities
            return await _dbSet.Where(e => nameProperty.Name == name)
                .FirstOrDefaultAsync();
        }
        else
        {
            // If it doesn't, throw an exception (or handle this however you like)
            throw new InvalidOperationException($"The {typeof(T).Name} class does not have both a Name property of type string and a TenantId property of type int.");
        }
    }

    public async Task<T> Add(T entity)
    {
        var createdAtProperty = typeof(T).GetProperty("CreatedAt");
        if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
        {
            createdAtProperty.SetValue(entity, DateTime.Now.ToUniversalTime());
        }

        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
    
        return entity;
    }
    
    public async Task<T> AddByTenantId(T entity, int tenantId)
    {
        var createdAtProperty = typeof(T).GetProperty("CreatedAt");
        var tenantIdProperty = typeof(T).GetProperty("TenantId");
    
        if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
        {
            createdAtProperty.SetValue(entity, DateTime.Now.ToUniversalTime());
        }

        if (tenantIdProperty != null && tenantIdProperty.PropertyType == typeof(int))
        {
            tenantIdProperty.SetValue(entity, tenantId);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        else
        {
            throw new InvalidOperationException($"The {typeof(T).Name} class does not have a TenantId property of type int.");
        }
    }

    public async Task<T> Update(T entity)
    {
        var updatedAtProperty = typeof(T).GetProperty("UpdatedAt");
        if (updatedAtProperty != null && updatedAtProperty.PropertyType == typeof(DateTime))
        {
            updatedAtProperty.SetValue(entity, DateTime.Now.ToUniversalTime());
        }

        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    
        return entity;
    }
    
    public async Task<T> UpdateByTenantId(T entity, int tenantId)
    {
        var updatedAtProperty = typeof(T).GetProperty("UpdatedAt");
        var tenantIdProperty = typeof(T).GetProperty("TenantId");
    
        if (updatedAtProperty != null && updatedAtProperty.PropertyType == typeof(DateTime))
        {
            updatedAtProperty.SetValue(entity, DateTime.Now.ToUniversalTime());
        }

        if (tenantIdProperty != null && tenantIdProperty.PropertyType == typeof(int))
        {
            var currentTenantId = (int)tenantIdProperty.GetValue(entity);
            if (currentTenantId == tenantId)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return entity;
            }
            else
            {
                throw new InvalidOperationException($"The provided TenantId does not match the TenantId of the entity.");
            }
        }
        else
        {
            throw new InvalidOperationException($"The {typeof(T).Name} class does not have a TenantId property of type int.");
        }
    }

    public async Task<T> Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
        _context.SaveChanges();
        
        return _dbSet.Find(entity);
    }
    
    public async Task<T> DeleteByTenantId(T entity, int tenantId)
    {
        var tenantIdProperty = typeof(T).GetProperty("TenantId");
        if (tenantIdProperty != null && tenantIdProperty.PropertyType == typeof(int))
        {
            var currentTenantId = (int)tenantIdProperty.GetValue(entity);
            if (currentTenantId == tenantId)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return _dbSet.Find(entity);
            }
            else
            {
                throw new InvalidOperationException($"The provided TenantId does not match the TenantId of the entity.");
            }
        }
        else
        {
            throw new InvalidOperationException($"The {typeof(T).Name} class does not have a TenantId property of type int.");
        }
    }
}