namespace UteamUP.Client.Web.Repository.Interfaces;

public interface ITagWebRepository
{
    Task<bool> CreateAsync(List<TagDto> tags);
}