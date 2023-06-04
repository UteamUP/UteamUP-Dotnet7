namespace UteamUP.Server.Api.Profiles;

public class PlanProfile : Profile
{
    public PlanProfile()
    {
        CreateMap<Plan, PlanDto>().ReverseMap();
        CreateMap<PlanDto, Plan>().ReverseMap();
    }
}