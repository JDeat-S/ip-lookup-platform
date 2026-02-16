using IpLookup.Api.Models.Entities;

namespace IpLookup.Api.Interfaces
{
    public interface IIpRepository
    {
        Task<List<IpQuery>> GetAllAsync();
        Task<IpQuery?> GetByIpAsync(string ipAddress);
        Task AddAsync(IpQuery ipQuery);
        Task DeleteAsync(int id);
    }
}
