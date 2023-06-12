namespace UteamUP.Server.Api.Profiles;

public class StockProfile : Profile
{
    public StockProfile()
    {
        CreateMap<Stock, StockDto>().ReverseMap();
        CreateMap<StockDto, Stock>().ReverseMap();
    }
}