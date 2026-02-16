using Microsoft.AspNetCore.Mvc;
using IpLookup.Api.Interfaces;
using IpLookup.Api.Models.DTOs;

namespace IpLookup.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IpController : ControllerBase
    {
        private readonly IIpService _ipService;

        public IpController(IIpService ipService)
        {
            _ipService = ipService;
        }

        // Consultar IP
        [HttpPost]
        public async Task<IActionResult> QueryIp([FromBody] string ipAddress)
        {
            try
            {
                var result = await _ipService.QueryIpAsync(ipAddress);
                return Ok(result);
            }
            catch (Exception ex)
            {
                if (ex.Message == "IP_ALREADY_EXISTS")
                    return Conflict("La IP ya existe.");

                return StatusCode(500, "Error consultando la IP.");
            }
        }


        // Obtener histórico (con filtro opcional)
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filter)
        {
            var result = await _ipService.GetAllAsync(filter);
            return Ok(result);
        }

        // Eliminar registro
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ipService.DeleteAsync(id);
            return NoContent();
        }
    }

}
