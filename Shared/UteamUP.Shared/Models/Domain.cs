namespace UteamUP.Shared.Models;

public class Domain : Base
{
    [Key] public int Id { get; set; }
    [Required] public string DomainName { get; set; }
    [ForeignKey("Tenant")] public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}