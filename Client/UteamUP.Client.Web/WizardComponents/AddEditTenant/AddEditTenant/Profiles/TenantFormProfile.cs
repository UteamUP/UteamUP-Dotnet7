using UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Forms;

namespace UteamUP.Client.Web.WizardComponents.AddEditTenant.AddEditTenant.Profiles;

public class TenantFormProfile : Profile
{
    public TenantFormProfile()
    {
        CreateMap<TenantForm, Tenant>()
            .ForMember(dest => dest.Id, act => act.MapFrom(src => src.TenantBasicForm.Id))
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.TenantBasicForm.Name))
            .ForMember(dest => dest.Website, act => act.MapFrom(src => src.TenantBasicForm.Website))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.TenantBasicForm.Description))
            .ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.TenantBasicForm.PhoneNumber))
            .ForMember(dest => dest.ContactEmail, act => act.MapFrom(src => src.TenantBasicForm.ContactEmail))
            .ForMember(dest => dest.Address, act => act.MapFrom(src => src.TenantAddressForm.Address))
            .ForMember(dest => dest.City, act => act.MapFrom(src => src.TenantAddressForm.City))
            .ForMember(dest => dest.Country, act => act.MapFrom(src => src.TenantAddressForm.Country))
            .ForMember(dest => dest.State, act => act.MapFrom(src => src.TenantAddressForm.State))
            .ForMember(dest => dest.PostalCode, act => act.MapFrom(src => src.TenantAddressForm.PostalCode))
            .ForMember(dest => dest.OwnerId, act => act.MapFrom(src => src.TenantOwnerForm.OwnerId))
            .ReverseMap();
        CreateMap<TenantForm, TenantDto>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => src.TenantBasicForm.Name))
            .ForMember(dest => dest.Website, act => act.MapFrom(src => src.TenantBasicForm.Website))
            .ForMember(dest => dest.Description, act => act.MapFrom(src => src.TenantBasicForm.Description))
            .ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.TenantBasicForm.PhoneNumber))
            .ForMember(dest => dest.ContactEmail, act => act.MapFrom(src => src.TenantBasicForm.ContactEmail))
            .ForMember(dest => dest.Address, act => act.MapFrom(src => src.TenantAddressForm.Address))
            .ForMember(dest => dest.City, act => act.MapFrom(src => src.TenantAddressForm.City))
            .ForMember(dest => dest.Country, act => act.MapFrom(src => src.TenantAddressForm.Country))
            .ForMember(dest => dest.State, act => act.MapFrom(src => src.TenantAddressForm.State))
            .ForMember(dest => dest.PostalCode, act => act.MapFrom(src => src.TenantAddressForm.PostalCode))
            .ForMember(dest => dest.OwnerId, act => act.MapFrom(src => src.TenantOwnerForm.OwnerId))
            .ReverseMap();
    }
}