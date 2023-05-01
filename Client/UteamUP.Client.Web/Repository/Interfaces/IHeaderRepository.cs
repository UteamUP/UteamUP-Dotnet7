using System.Net.Http.Headers;

namespace UteamUP.Client.Web.Repository.Interfaces;

public interface IHeaderRepository
{ 
    Task<AuthenticationHeaderValue> GetHeaderAsync();
}