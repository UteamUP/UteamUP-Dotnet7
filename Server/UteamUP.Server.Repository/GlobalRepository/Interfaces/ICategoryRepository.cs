namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> CreateAsync(List<CategoryDto> categories, string oid, int tenantId);
}