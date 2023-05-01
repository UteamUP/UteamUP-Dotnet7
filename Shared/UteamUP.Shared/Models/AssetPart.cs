namespace UteamUP.Shared.Models;

public class AssetPart : Base
{
    [Key] public int Id { get; set; }
    [ForeignKey("AssetId")] public int AssetId { get; set; }
    public virtual Asset Asset { get; set; }

    [ForeignKey("PartId")] public int PartId { get; set; }
    public virtual Part Part { get; set; }
}