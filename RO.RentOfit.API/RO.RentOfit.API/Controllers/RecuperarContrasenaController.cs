
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
        public async Task<IActionResult> ValidarToken(string email, string token)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ValidarToken(email, token));
        }



        [HttpPost("ActualizarContrasena")]
        public async Task<IActionResult> ActualizarContrasena(string contrasena, string email)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ActualizarContrasena(contrasena, email));
        }

    }
}
