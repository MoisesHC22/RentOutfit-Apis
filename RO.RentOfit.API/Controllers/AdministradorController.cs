﻿
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdministradorController : ApiController
    {
        private readonly IConfiguration _configuration;

        public AdministradorController(IApiController appController, IConfiguration configuration) : base(appController)
        {
            _configuration = configuration;
        }


        // Método para consultar establecimientos pendientes de aprobación
        [HttpPost("EstablecimientosParaAprobacion")]
        public async Task<IActionResult> PostEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters)
        {
            var (establecimientos, totalRegistros) =  await _appController.administradorPresenter.ConsultarEstablecimientosParaAprobacion(parameters);

            return Ok(new
            {
                Establecimientos = establecimientos,
                TotalRegistros = totalRegistros
            });
        }



        // Método para aprobar un establecimiento
        [HttpPost("AprobarEstablecimiento")]
        public async Task<IActionResult> PostAprobarEstablecimiento([FromBody] int establecimientoId)
        {
            return Ok(await _appController.administradorPresenter.AprobarEstablecimiento(establecimientoId));
        }



        // Método para denegar un establecimiento
        [HttpPost("DenegarEstablecimiento")]
        public async Task<IActionResult> PostDenegarEstablecimiento(MotivosDenegarAggregate requerimientos)
        {
            return Ok(await _appController.administradorPresenter.DenegarEstablecimiento(requerimientos));
        }


        [HttpPost("TodosLosEstablecimientos")]
        public async Task<IActionResult> TodosLosEstablecimientos(TodosEstablecimientosAggregate requerimientos)
        {
            return Ok(await _appController.administradorPresenter.TodosLosEstablecimientos(requerimientos));
        }

        [HttpPost("Alertar")]
        public async Task<IActionResult> Alertar(MandarMsj requerimientos)
        {
            await _appController.administradorPresenter.Alertar(requerimientos);
            return Ok(new { mensaje = "Alerta enviada exitosamente" });
        }
    }
}
