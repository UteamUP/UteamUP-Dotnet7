namespace UteamUP.Shared.ModelDto;

public class TenantDto
{
    [MaxLength(255)] [MinLength(2)] public string Name { get; set; }

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

    [ForeignKey("MUser")] public int OwnerId { get; set; }
}