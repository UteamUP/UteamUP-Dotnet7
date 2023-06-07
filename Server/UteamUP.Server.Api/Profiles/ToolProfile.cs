namespace UteamUP.Server.Api.Profiles;

public class ToolProfile : Profile
{
    public ToolProfile()
    {
        CreateMap<Tool, ToolDto>().ReverseMap();
        CreateMap<ToolDto, Tool>().ReverseMap();
    }
}