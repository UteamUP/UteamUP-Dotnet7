namespace UteamUP.Shared.Models;

public class LicenseUser : Base
{
    [Key] public int Id { get; set; }

    [ForeignKey("LicenseId")] public int LicenseId { get; set; }
    [ForeignKey("UserId")] public int UserId { get; set; }

    public License License { get; set; }
    public MUser MUser { get; set; }
}