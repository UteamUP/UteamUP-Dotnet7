namespace UteamUP.Server.Profiles;

public class TenantProfile : Profile
{
    public TenantProfile()
    {
        CreateMap<Tenant, TenantDto>();
        CreateMap<TenantDto, Tenant>();
    }
}