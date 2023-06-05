namespace UteamUP.Shared.ModelDto;

public class LocationTagDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int TenantId { get; set; }
    public List<TagDto> Tags { get; set; }
}