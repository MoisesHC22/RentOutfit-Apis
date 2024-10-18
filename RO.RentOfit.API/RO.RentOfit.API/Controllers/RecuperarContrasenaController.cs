
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecuperarContrasenaController : ApiController
    {
        private readonly IConfiguration _configuration;

        public RecuperarContrasenaController(IApiController appController, IConfiguration configuration) : base(appController)
        {
            _configuration = configuration;
        }

        [HttpPost("ObtenerToken")]
        public async Task<IActionResult> ObtenerToken(string email)
        {
            return Ok( await _appController.recuperarContrasenaPresenter.ObtenerToken(email));
        }



        [HttpPost("ValidarToken")]
        public async Task<IActionResult> ValidarToken(RecuperarContrasenaAggregate requerimientos)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ValidarToken(requerimientos.email, requerimientos.token));
        }



        [HttpPost("ActualizarContrasena")]
        public async Task<IActionResult> ActualizarContrasena(RecuperarContrasenaAggregate requerimientos)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ActualizarContrasena(requerimientos.contrasena, requerimientos.email));
        }

    }
}
