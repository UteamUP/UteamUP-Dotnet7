namespace UteamUP.Server.Api.Profiles;

public class TenantProfile : Profile
{
    public TenantProfile()
    {
        CreateMap<Tenant, TenantDto>().ReverseMap();
    }
}