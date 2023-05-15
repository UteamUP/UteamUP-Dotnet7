namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> CreateAsync(List<CategoryDto> categories, MUserDto? userDto, int tenantId);
    Task<List<Category>> GetAllCategoriesByTenantIdAsync(int tenantId);
}