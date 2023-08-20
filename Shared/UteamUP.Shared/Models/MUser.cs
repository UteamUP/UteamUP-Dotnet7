using System.Text.Json.Serialization;

namespace UteamUP.Shared.Models;

public class MUser : Base
{
    [Key] public int Id { get; set; }

    [MaxLength(255)]
    [MinLength(2)]
    [Required(ErrorMessage = "You must specify the user name.")]
    public string? Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "You must enter a valid email")]
    public string? Email { get; set; } = string.Empty;

    public string? DepartmentCompany { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public int DefaultTenantId { get; set; }

    [Required]
    [MaxLength(50)]
    [MinLength(5)]
    public string Oid { get; set; } = string.Empty;

    public bool IsAdmin { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
    public bool HasBeenActivated { get; set; } = false;
    public bool IsFirstLogin { get; set; } = true;
    public bool HasAcceptedLicenseAgreement { get; set; } = false;
    public string ImageUrl { get; set; } = string.Empty;

    public string ActivationCode { get; set; } = string.Empty;
    [MaxLength(50)] [MinLength(2)] public string DefaultLanguage { get; set; } = "en";
    //public virtual HashSet<Tag>? Tags { get; set; } = new();
    // Address information
    public string? Country { get; set; } = string.Empty;
    public string? StreetName { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;

    // Other information
    public string Website { get; set; } = string.Empty;

    [JsonIgnore]
    public virtual ICollection<Tenant>? Tenants { get; set; }
}