namespace UteamUP.Shared.ModelDto;

public class LocationDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public virtual Tenant? Tenant { get; set; }
}