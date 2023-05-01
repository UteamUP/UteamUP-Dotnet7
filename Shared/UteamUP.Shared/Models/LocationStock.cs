namespace UteamUP.Shared.Models;

public class LocationStock : Base
{
    [Key] public int Id { get; set; }

    [ForeignKey("LocationId")] public int LocationId { get; set; }
    public Location Location { get; set; }

    [ForeignKey("StockId")] public int StockId { get; set; }
    public Stock Stock { get; set; }
}