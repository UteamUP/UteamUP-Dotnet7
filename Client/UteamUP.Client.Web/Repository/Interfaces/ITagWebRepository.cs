namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ITagWebRepository
{
    Task<bool> CreateManyAsync(List<TagDto> tags);
    Task<Tag> CreateAsync(TagDto tag);
    Task<Tag> GetTagByNameAsync(string name);
    Task<Tag> GetTagByNameAndLocationNameAsync(string tagName, string locationName);
    Task<Tag> GetTagByNameAndTenantIdAsync(string tagName, int tenantId);
    Task<Tag> GetOrCreateTagAsync(string name, int tenantId);
}