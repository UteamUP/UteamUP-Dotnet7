namespace UteamUP.Server.Api.Profiles;

public class InvitedUserProfile : Profile
{
    public InvitedUserProfile()
    {
        CreateMap<InvitedUser, InvitedUserDto>().ReverseMap();
        CreateMap<InvitedUserDto, InvitedUser>().ReverseMap();
    }
}