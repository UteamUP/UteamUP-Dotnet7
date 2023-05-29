namespace UteamUP.Shared.Models;

public class Location : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the location name before you can save.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    public virtual Tenant? Tenant { get; set; }
    public List<Tag> Tags { get; set; }
    
    //public virtual List<Asset>? Assets { get; set; }
    //public virtual List<Stock>? Stocks { get; set; }
    //public virtual GPS? GPS { get; set; }

}