namespace UteamUP.Shared.Models;

public class Vendor : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(512)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the name before you can save.")]
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string WebSite { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public bool Approved { get; set; } = false;
    public MUser Creator { get; set; } = null!;
    public MUser? UpdatedBy { get; set; }
}