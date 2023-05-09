namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IVendorWebRepository
{
    Task<bool> CreateAsync(VendorDto tenant);
    Task<List<Vendor>> GetAllAsync();
}