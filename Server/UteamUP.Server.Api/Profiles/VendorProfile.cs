namespace UteamUP.Server.Api.Profiles;

public class VendorProfile : Profile
{
    public VendorProfile()
    {
        CreateMap<Vendor, VendorDto>().ReverseMap();
    }
}