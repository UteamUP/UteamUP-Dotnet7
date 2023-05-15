namespace UteamUP.Shared.Models;

public class StockItemLog : Base
{
    [Key] public int Id { get; set; }
    public StockItemPart? StockItemParts { get; set; }
    public Asset? Asset { get; set; }
    public MUser? User { get; set; }
    public int AmountRemoved { get; set; }
}