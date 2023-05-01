namespace UteamUP.Shared.Models;

public class InvitedUser : Base
{
    [Key] public int Id { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Guid { get; set; }

    [ForeignKey("Tenant")] public int TenantId { get; set; }
    public Tenant? Tenant { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public bool IsRegistered { get; set; } = false;
    public bool IsArchived { get; set; } = false;
}