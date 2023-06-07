namespace UteamUP.Shared.Models;

public class Tag : Base
{
    [Key] public int Id { get; set; }
    public string Name { get; set; }
    
    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    public virtual Tenant? Tenant { get; set; }
    
    public ICollection<LocationTag>? LocationTags { get; }
    public ICollection<ToolTag>? ToolTags { get; }
}