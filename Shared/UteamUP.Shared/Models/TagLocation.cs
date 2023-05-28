namespace UteamUP.Shared.Models;

public class TagLocation
{
    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}