namespace UteamUP.Shared.Models;

public class Location : Base
{
    public Location()
    {
        Stocks = new List<Stock>();
        Assets = new List<Asset>();
        Tags = new List<Tag>();
    }

    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the location name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    [ForeignKey("Tenant")] public int? TenantId { get; set; }

    [Required(ErrorMessage = "The tenant must be selected before you can save.")]
    public Tenant Tenant { get; set; }
    public virtual List<Asset>? Assets { get; set; } = new();
    public virtual List<Stock>? Stocks { get; set; }
    public virtual List<Tag>? Tags { get; set; }
}