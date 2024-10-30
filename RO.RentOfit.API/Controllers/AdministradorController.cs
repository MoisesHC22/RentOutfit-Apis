
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
            return Ok( await _appController.administradorPresenter.ConsultarEstablecimientosParaAprobacion(parameters));
        }



        // Método para aprobar un establecimiento
        [HttpPost("AprobarEstablecimiento")]
        public async Task<IActionResult> PostAprobarEstablecimiento([FromBody] int establecimientoId)
        {
            return Ok(await _appController.administradorPresenter.AprobarEstablecimiento(establecimientoId));
        }



        // Método para denegar un establecimiento
        [HttpPost("DenegarEstablecimiento")]
        public async Task<IActionResult> PostDenegarEstablecimiento([FromBody] int establecimientoId)
        {
            return Ok(await _appController.administradorPresenter.DenegarEstablecimiento(establecimientoId));
        }
    }
}
