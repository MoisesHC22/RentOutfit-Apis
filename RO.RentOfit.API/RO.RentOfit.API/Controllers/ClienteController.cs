
using NPOI.OpenXmlFormats.Dml;

namespace RO.RentOfit.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}
