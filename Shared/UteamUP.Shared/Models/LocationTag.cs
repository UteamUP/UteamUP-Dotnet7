using System.Text.Json.Serialization;

namespace UteamUP.Shared.Models;

public class LocationTag
{
    [Key] public int Id { get; set; }

    [ForeignKey("Location")] public int LocationId { get; set; }
    [JsonIgnore]
    public Location Location { get; set; }

    [ForeignKey("Tag")] public int TagId { get; set; }
    [JsonIgnore]
    public Tag Tag { get; set; }
}