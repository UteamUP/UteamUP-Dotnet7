namespace UteamUP.Shared.Models;

public class Stock : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; } = string.Empty;

    public string Guid { get; set; } = string.Empty;
    public string RackBarNumber { get; set; } = string.Empty;
    public string ShelveNumber { get; set; } = string.Empty;
    public string ShelveName { get; set; } = string.Empty;

    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    
    [ForeignKey("Location")] public int? LocationId { get; set; }
    public Location? Location { get; set; }

    [ForeignKey("Category")] public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}