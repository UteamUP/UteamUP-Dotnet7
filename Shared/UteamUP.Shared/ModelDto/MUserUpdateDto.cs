namespace UteamUP.Shared.ModelDto;

public class MUserUpdateDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? DepartmentCompany { get; set; }
    public string? Phone { get; set; }
    public int DefaultTenantId { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
    public bool HasBeenActivated { get; set; }
    public bool IsFirstLogin { get; set; }
    public bool HasAcceptedLicenseAgreement { get; set; }

    public string? ActivationCode { get; set; }

    // Address information
    public string? Country { get; set; } = string.Empty;
    public string? StreetName { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? PostalCode { get; set; } = string.Empty;
    // Other information
    public string? Website { get; set; } = string.Empty;
}