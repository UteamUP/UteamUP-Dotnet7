namespace UteamUP.Shared.Models;

public class LicenseUsers : Base
{
    [Key] public int Id { get; set; }

    [ForeignKey("LicenseId")] public int LicenseId { get; set; }
    [ForeignKey("MUserId")] public int MUserId { get; set; }

    public License License { get; set; }
    public MUser MUser { get; set; }
}