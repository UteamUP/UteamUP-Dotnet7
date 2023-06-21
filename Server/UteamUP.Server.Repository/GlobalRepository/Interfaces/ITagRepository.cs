using UteamUP.Shared.Results;

namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> CreateManyAsync(List<TagDto> tags);
    Task<TagDataResult<Tag>> GetAllTagsByTenantIdAsync(int tenantId, string filter, string sort, int skip, int top);
    Task<Tag> CreateAsync(Tag tag);
    Task<Tag> GetTagByNameAsync(string name);
    Task<Tag> GetTagByNameAndLocationNameAsync(string name, string locationName);
    Task<Tag> GetTagByNameAndTenantIdAsync(string tagName, int tenantId);

}