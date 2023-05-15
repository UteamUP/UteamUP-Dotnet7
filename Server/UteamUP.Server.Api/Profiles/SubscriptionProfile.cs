namespace UteamUP.Server.Profiles;

public class SubscriptionProfile : Profile
{
    public SubscriptionProfile()
    {
        CreateMap<Subscription, SubscriptionDto>();
        CreateMap<SubscriptionDto, Subscription>();
    }
}