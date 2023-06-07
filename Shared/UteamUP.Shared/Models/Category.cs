namespace UteamUP.Shared.Models;

public class Category : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the category name before you can save.")]
    public string Name { get; set; } = string.Empty;

    [ForeignKey("Tenant")] public int? TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    
    [ForeignKey("MUser")] public int? CreatorId { get; set; }
    public MUser? Creator { get; set; } // To show who created the category
}