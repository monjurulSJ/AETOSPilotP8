using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P8.Service.Services;

namespace AETOS.P8.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AetosController : ControllerBase
    {
        private readonly IAetosService _aetosService;
        public AetosController(IAetosService aetosService)
        {
            _aetosService = aetosService;
        }
        [HttpGet("GetVehicles")]
        public async Task<IActionResult> GetVehicles(DateTime startTime, DateTime endTime, int speed)
        {
            try
            {
                var response = await _aetosService.GetVehicles(startTime, endTime, speed);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetTemperatures")]
        public async Task<IActionResult> GetTemperatures(DateTime targetDate)
        {
            try
            {
                var response = await _aetosService.GetTemperatures(targetDate);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
