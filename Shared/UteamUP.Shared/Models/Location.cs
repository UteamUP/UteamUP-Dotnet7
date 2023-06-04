namespace UteamUP.Shared.Models;

public class Location : Base
{
    public Location()
    {
        LocationTags = new List<LocationTag>();
    }
    
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the location name before you can save.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    [ForeignKey("Tenant")] public int TenantId { get; set; }
    public virtual Tenant? Tenant { get; set; }
    public ICollection<LocationTag> LocationTags { get; set; }
    
}