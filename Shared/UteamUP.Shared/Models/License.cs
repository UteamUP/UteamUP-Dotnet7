namespace UteamUP.Shared.Models;

public class License : Base
{
    [Key] public int Id { get; set; }
    public int MaxLicenses { get; set; }
    public int MinLicenses { get; set; }

    [ForeignKey("Subscription")] public int SubscriptionId { get; set; }
    public Subscription Subscription { get; set; }
    
    public virtual ICollection<MUser>? Users { get; set; }
}