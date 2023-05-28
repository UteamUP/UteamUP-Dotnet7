namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> CreateAsync(List<TagDto> tags);
    Task<Tag> GetTagByNameAsync(string name);
    Task<Tag> GetTagByNameAndLocationNameAsync(string name, string locationName);
    Task<Tag> GetTagByNameAndTenantIdAsync(string tagName, int tenantId);

}