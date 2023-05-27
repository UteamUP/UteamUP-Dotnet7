namespace UteamUP.Server.Repository.GlobalRepository.Interfaces;

public interface IPartRepository
{
    Task<Part?> CreatePartAsync(PartDto part, int tenantId = 0);
}