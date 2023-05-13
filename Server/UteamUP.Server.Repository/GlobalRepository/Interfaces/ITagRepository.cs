namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface ITagRepository
{
    Task<List<Tag>> CreateAsync(List<TagDto> tags);
}