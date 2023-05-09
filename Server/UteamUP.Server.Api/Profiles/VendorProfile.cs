namespace UteamUP.Server.Profiles;

public class VendorProfile : Profile
{
    public VendorProfile()
    {
        CreateMap<Vendor, VendorDto>();
        CreateMap<VendorDto, Vendor>();
    }
}