
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RecuperarContrasenaController : ApiController
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;  // Servicio de correo electrónico
        private readonly PasswordService _passwordService; // Servicio de manejo de contraseñas

        // Inyectamos ambos servicios (email y password)
        public RecuperarContrasenaController(IApiController appController, IConfiguration configuration, EmailService emailService, PasswordService passwordService)
            : base(appController)
        {
            _configuration = configuration;
            _emailService = emailService;
            _passwordService = passwordService;  // Inyectamos el servicio de contraseñas
        }

        [HttpPost("ObtenerToken")]
        public async Task<IActionResult> ObtenerToken(RequerimientosCorreoAggregate requerimiento)
        {
            // Obtener el token para recuperación de contraseña
            return Ok(await _appController.recuperarContrasenaPresenter.ObtenerToken(requerimiento.email));
        }


        [HttpPost("ValidarToken")]
        public async Task<IActionResult> ValidarToken(ValidarToken requerimientos)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ValidarToken(requerimientos));
        }


        [HttpPost("ActualizarContrasena")]
        public async Task<IActionResult> ActualizarContrasena(ActualizarContrasena requerimimientos)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ActualizarContrasena(requerimimientos));
        }
    }
}
