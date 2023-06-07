namespace UteamUP.Server.Api.Profiles;

public class TenantProfile : Profile
{
    public TenantProfile()
    {
        CreateMap<Tenant, TenantDto>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Name))
            .ForMember(src => src.Description, dst => dst.MapFrom(e => e.Description))
            .ForMember(src => src.Address, dst => dst.MapFrom(e => e.Address))
            .ForMember(src => src.City, dst => dst.MapFrom(e => e.City))
            .ForMember(src => src.State, dst => dst.MapFrom(e => e.State))
            .ForMember(src => src.Country, dst => dst.MapFrom(e => e.Country))
            .ForMember(src => src.PostalCode, dst => dst.MapFrom(e => e.PostalCode))
            .ForMember(src => src.ContactEmail, dst => dst.MapFrom(e => e.ContactEmail))
            .ForMember(src => src.PhoneNumber, dst => dst.MapFrom(e => e.PhoneNumber))
            .ForMember(src => src.Website, dst => dst.MapFrom(e => e.Website))
            .ForMember(src => src.IsActive, dst => dst.MapFrom(e => e.IsActive))
            .ForMember(src => src.OwnerId, dst => dst.MapFrom(e => e.OwnerId))
            .ForMember(src => src.SubscriptionId, dst => dst.MapFrom(e => e.SubscriptionId))
            .ReverseMap();
        CreateMap<TenantDto, Tenant>()
            .ForMember(src => src.Name, dst => dst.MapFrom(e => e.Name))
            .ForMember(src => src.Description, dst => dst.MapFrom(e => e.Description))
            .ForMember(src => src.Address, dst => dst.MapFrom(e => e.Address))
            .ForMember(src => src.City, dst => dst.MapFrom(e => e.City))
            .ForMember(src => src.State, dst => dst.MapFrom(e => e.State))
            .ForMember(src => src.Country, dst => dst.MapFrom(e => e.Country))
            .ForMember(src => src.PostalCode, dst => dst.MapFrom(e => e.PostalCode))
            .ForMember(src => src.ContactEmail, dst => dst.MapFrom(e => e.ContactEmail))
            .ForMember(src => src.PhoneNumber, dst => dst.MapFrom(e => e.PhoneNumber))
            .ForMember(src => src.Website, dst => dst.MapFrom(e => e.Website))
            .ForMember(src => src.IsActive, dst => dst.MapFrom(e => e.IsActive))
            .ForMember(src => src.OwnerId, dst => dst.MapFrom(e => e.OwnerId))
            .ForMember(src => src.SubscriptionId, dst => dst.MapFrom(e => e.SubscriptionId))
            .ReverseMap();

    }
}