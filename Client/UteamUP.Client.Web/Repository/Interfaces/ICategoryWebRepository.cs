namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ICategoryWebRepository
{
    Task<bool> CreateAsync(List<CategoryDto> categories, int id);
    Task<List<Category>> GetAllCategoriesByTenantIdAsync(int tenantId);
}