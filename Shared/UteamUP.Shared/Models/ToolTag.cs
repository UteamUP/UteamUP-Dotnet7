using System.Text.Json.Serialization;

namespace UteamUP.Shared.Models;

public class ToolTag
{
    [Key] public int Id { get; set; }

    [ForeignKey("Tool")] public int ToolId { get; set; }
    [JsonIgnore]
    public Tool? Tool { get; set; }

    [ForeignKey("Tag")] public int TagId { get; set; }
    [JsonIgnore]
    public Tag? Tag { get; set; }
}