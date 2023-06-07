namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> CreateMultipleAsync(List<CategoryDto> categories, MUserDto? userDto, int tenantId);
    Task<Category> CreateAsync(CategoryDto category, int tenantId, string oid);
    Task<Category> UpdateAsync(CategoryDto category, int id, int tenantId, string oid);
    Task<List<Category>> GetAllCategoriesByTenantIdAsync(int tenantId);
    Task<Category?> GetCategoryByIdAndTenantIdAsync(int id, int tenantId);
}