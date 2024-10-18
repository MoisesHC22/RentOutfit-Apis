using RO.RentOfit.API.Services;
using RO.RentOfit.Infraestructure.Security;

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
        public async Task<IActionResult> ObtenerToken(string email)
        {
            // Obtener el token para recuperación de contraseña
            var resultado = await _appController.recuperarContrasenaPresenter.ObtenerToken(email);

            if (resultado == null || string.IsNullOrEmpty(resultado.token))
            {
                return BadRequest("No se pudo generar el token.");
            }

            // Usamos el servicio de email para enviar el correo de recuperación con el token
            await _emailService.EnviarEmailRecuperacionContrasenaAsync(email, resultado.token);

            return Ok("Correo de recuperación enviado.");
        }

        [HttpPost("ValidarToken")]
        public async Task<IActionResult> ValidarToken(string email, string token)
        {
            return Ok(await _appController.recuperarContrasenaPresenter.ValidarToken(email, token));
        }

        [HttpPost("ActualizarContrasena")]
        public async Task<IActionResult> ActualizarContrasena(string contrasena, string email)
        {
            // Hashear la nueva contraseña
            var hashedPassword = _passwordService.HashPassword(contrasena);

            // Actualizar la contraseña en la base de datos
            var resultado = await _appController.recuperarContrasenaPresenter.ActualizarContrasena(hashedPassword, email);

            if (resultado == null || resultado.tipoError != 0)
            {
                return BadRequest("No se pudo actualizar la contraseña.");
            }

            return Ok("Contraseña actualizada con éxito.");
        }
    }
}
