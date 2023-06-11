using System.Text.Json.Serialization;

namespace UteamUP.Shared.Models;

public class StockTag
{
    [Key] public int Id { get; set; }

    [ForeignKey("Stock")] public int StockId { get; set; }
    [JsonIgnore]
    public Stock Stock { get; set; }

    [ForeignKey("Tag")] public int TagId { get; set; }
    [JsonIgnore]
    public Tag Tag { get; set; }
}