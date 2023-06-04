namespace UteamUP.Server.Api.Profiles;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Subscription, SubscriptionDto>().ReverseMap();
    }
}