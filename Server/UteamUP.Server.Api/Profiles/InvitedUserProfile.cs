namespace UteamUP.Server.Profiles;

public class InvitedUserProfile : Profile
{
    public InvitedUserProfile()
    {
        CreateMap<InvitedUser, InvitedUserDto>();
        CreateMap<InvitedUserDto, InvitedUser>();
    }
}