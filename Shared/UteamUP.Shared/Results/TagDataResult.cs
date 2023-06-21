namespace UteamUP.Shared.Results;

public class TagDataResult<T>
{
    public IEnumerable<T> Data { get; set; }
    public int Count { get; set; }
}