
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ApiController
    {
        public ClienteController(IApiController appController) : base(appController) 
        {
        }

        [HttpPost("ObtenerCliente")]
        public async ValueTask<IActionResult> ObtenerCliente([FromBody] int usuarioID)
        {
            return Ok( await _appController.ClientePresenter.ObtenerCliente(usuarioID));
        }

        [HttpPost("RegistrarCliente")]
        public async ValueTask<IActionResult> RegistrarCliente([FromBody] RegistrarClienteAggregate registro)
        {
            return Ok( await _appController.ClientePresenter.RegistrarCliente(registro));
        }

        [HttpPost("IniciarSesion")]
        public async ValueTask<IActionResult> IniciarSesion([FromBody] IniciarSesionAggregate requerimiento)
        {
            return Ok(await _appController.ClientePresenter.IniciarSesion(requerimiento));
        }

    }
}
