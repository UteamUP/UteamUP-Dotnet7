namespace UteamUP.Server.Profiles;

public class PlanProfile : Profile
{
    public PlanProfile()
    {
        CreateMap<Plan, PlanDto>();
        CreateMap<PlanDto, Plan>();
    }
}