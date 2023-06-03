namespace UteamUP.Shared.ModelDto;

public class LocationDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int? TenantId { get; set; }
    public List<TagDto>? Tags { get; set; }
}