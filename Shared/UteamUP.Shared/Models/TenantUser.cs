namespace UteamUP.Shared.Models;

public class TenantUser : Base
{
    [Key] public int Id { get; set; }

    [ForeignKey("TenantId")] public int TenantId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }

    public Tenant Tenant { get; set; }
    public MUser MUser { get; set; }
}