namespace UteamUP.Shared.Models;

public class StockTools : Base
{
    [Key] public int Id { get; set; }
    
    [ForeignKey("Tenant")] public int TenantId { get; set; }
    public virtual Tenant? Tenant { get; set; }
    
    [ForeignKey("Stock")] public int StockId { get; set; }
    public virtual Stock? Stock { get; set; }
    
    public Tool Tools { get; set; }
    public int? QuantityAvailable { get; set; }
    public int? QuantityMustHave { get; set; }
    
    public DateTime? PurchaseDate { get; set; }
    public float? PurchasePrice { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    public bool IsDamaged { get; set; } = false;
    public bool IsLost { get; set; } = false;
    public bool IsInUse { get; set; } = false;
    public bool IsReserved { get; set; } = false;
    public bool IsBroken { get; set; } = false;
    public bool IsExpired { get; set; } = false;
}