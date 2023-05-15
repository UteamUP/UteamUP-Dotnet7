namespace UteamUP.Shared.Models;

public class Tenant : Base
{
    public Tenant()
    {
        Users = new List<MUser>();
    }

    private IEnumerable<string>? _userNames;
    [Key] public int Id { get; set; }

    [MaxLength(255)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify a name for the tenant. It can be the name of the organization.")]
    [Display(Name = "Tenant Name", Order = 0, Prompt = "Enter Tenant Name", Description = "Tenant's Name")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    [MaxLength(255)] [MinLength(3)] public string Address { get; set; } = string.Empty;
    [MaxLength(255)] [MinLength(2)] public string City { get; set; } = string.Empty;
    [MaxLength(255)] [MinLength(2)] public string State { get; set; } = string.Empty;
    [MaxLength(255)] [MinLength(2)] public string Country { get; set; } = string.Empty;

    [MaxLength(255)]
    [MinLength(2)]
    // Is also known as ZIP
    public string PostalCode { get; set; } = string.Empty;

    [MaxLength(512)]
    [MinLength(5)]
    [EmailAddress(ErrorMessage = "You must enter a valid email")]
    public string ContactEmail { get; set; } = string.Empty;

    [MaxLength(150)]
    [MinLength(5)]
    [Phone(ErrorMessage = "You must enter a valid phone number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [MaxLength(512)]
    [MinLength(7)]
    [Url(ErrorMessage = "You must enter a valid url, example http://www.example.com")]
    public string Website { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    public bool Deleted { get; set; } = false;
    public string ImageUrl { get; set; } = string.Empty;

    [ForeignKey("MUser")] public int OwnerId { get; set; }

    public virtual ICollection<MUser>? Users { get; set; }

    public virtual ICollection<Location>? Locations { get; set; }
}