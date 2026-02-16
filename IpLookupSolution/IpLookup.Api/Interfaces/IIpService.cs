using IpLookup.Api.Models.DTOs;

public interface IIpService
{
    Task<IpQueryDto> QueryIpAsync(string ipAddress);
    Task<List<IpQueryDto>> GetAllAsync(string? filter = null);
    Task DeleteAsync(int id);
}
