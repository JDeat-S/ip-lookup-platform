using IpLookup.Api.Interfaces;
using IpLookup.Api.Models.DTOs;
using IpLookup.Api.Models.Entities;
using System.Net;
using System.Runtime;

namespace IpLookup.Api.Services
{
    public class IpService : IIpService
    {
        private readonly IIpRepository _repository;
        private readonly HttpClient _httpClient;

        public IpService(
            IIpRepository repository,
            IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<IpQueryDto> QueryIpAsync(string ipAddress)
        {
            // 1️ Verificar si ya existe
            var existing = await _repository.GetByIpAsync(ipAddress);

            if (existing != null)
            {
                throw new Exception("IP_ALREADY_EXISTS");
            }

            //Consultar API externa
            var response = await _httpClient
                .GetFromJsonAsync<IpWhoIsResponseDto>($"https://ipwho.is/{ipAddress}");

            if (response == null || !response.Success)
                throw new Exception("IP_NOT_FOUND");
            
            var threatLevels = new[] { "Low", "Medium", "High" };
            var random = new Random();
            var randomThreat = threatLevels[random.Next(threatLevels.Length)];

            //Crear entidad (solo 7 columnas)
            var entity = new IpQuery
            {
                IpAddress = response.Ip,
                Country = response.Country,
                City = response.City,
                ISP = response.Connection?.Isp,
                Latitude = response.Latitude,
                Longitude = response.Longitude,
                ThreatLevel = randomThreat,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(entity);

            //Retornar DTO
            return MapToDto(entity);
        }

        public async Task<List<IpQueryDto>> GetAllAsync(string? filter = null)
        {
            var entities = await _repository.GetAllAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();

                entities = entities.Where(e =>
                    e.IpAddress.ToLower().Contains(filter) ||
                    e.Country.ToLower().Contains(filter) ||
                    e.City.ToLower().Contains(filter) ||
                    e.ISP.ToLower().Contains(filter)
                ).ToList();
            }

            return entities.Select(MapToDto).ToList();
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private IpQueryDto MapToDto(IpQuery entity)
        {
            return new IpQueryDto
            {
                Id = entity.Id,
                IpAddress = entity.IpAddress,
                Country = entity.Country,
                City = entity.City,
                ISP = entity.ISP,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                ThreatLevel = entity.ThreatLevel,
                CreatedAt = entity.CreatedAt
            };
        }
    }

}
