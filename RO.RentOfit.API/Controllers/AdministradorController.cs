using Microsoft.AspNetCore.Mvc;
using RO.RentOfit.Domain.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using RO.RentOfit.Domain.DTOs.Establecimientos;
using System.Threading.Tasks;
using RO.RentOfit.Domain.DTOs;

namespace RO.RentOfit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorPresenter _administradorPresenter;
        private readonly IConfiguration _configuration;

        public AdministradorController(IAdministradorPresenter administradorPresenter, IConfiguration configuration)
        {
            _administradorPresenter = administradorPresenter;
            _configuration = configuration;
        }

        // Método para consultar establecimientos pendientes de aprobación
        [HttpPost("EstablecimientosParaAprobacion")]
        public async Task<IActionResult> PostEstablecimientosParaAprobacion([FromBody] EstablecimientosParaAprobacionParams parameters)
        {
            if (parameters.Pagina < 1)
            {
                return BadRequest(new { message = "El número de página debe ser mayor o igual a 1." });
            }

            try
            {
                var result = await _administradorPresenter.ConsultarEstablecimientosParaAprobacion(parameters);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Opción para registrar el error en logs
                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud", error = ex.Message });
            }
        }

        // Método para aprobar un establecimiento
        [HttpPost("AprobarEstablecimiento")]
        public async Task<IActionResult> PostAprobarEstablecimiento([FromBody] int establecimientoId)
        {
            if (establecimientoId <= 0)
            {
                return BadRequest(new { message = "ID de establecimiento no válido." });
            }

            try
            {
                var resultado = await _administradorPresenter.AprobarEstablecimiento(establecimientoId);
                return resultado.tipoError == 0 ? Ok(resultado) : BadRequest(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al aprobar el establecimiento", error = ex.Message });
            }
        }

        // Método para denegar un establecimiento
        [HttpPost("DenegarEstablecimiento")]
        public async Task<IActionResult> PostDenegarEstablecimiento([FromBody] int establecimientoId)
        {
          
            try
            {
                var resultado = await _administradorPresenter.DenegarEstablecimiento(establecimientoId);
                return resultado.tipoError == 0 ? Ok(resultado) : BadRequest(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al denegar el establecimiento", error = ex.Message });
            }
        }
    }
}
