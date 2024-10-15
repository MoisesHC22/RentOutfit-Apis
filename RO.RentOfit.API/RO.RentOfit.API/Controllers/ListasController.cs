
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ListasController : ApiController
    {
        public ListasController(IApiController appController) : base(appController) 
        {
        }

        [HttpPost("ObtenerGeneros")]
        public async ValueTask<IActionResult> ObtenerGeneros()
        {
            return Ok( await _appController.ListasPresenter.ObtenerGeneros());
        }

        [HttpPost("ObtenerEstados")]
        public async ValueTask<IActionResult> ObtenerEstados()
        {
            return Ok(await _appController.ListasPresenter.ObtenerEstados());
        }

        [HttpPost("ObtenerMunicipios")]
        public async ValueTask<IActionResult> ObtenerMunicipios([FromBody] int estadoID)
        {
            return Ok(await _appController.ListasPresenter.ObtenerMunicipios(estadoID));
        }

        [HttpPost("ObtenerTallas")]
        public async ValueTask<IActionResult> ObtenerTallas()
        {
            return Ok(await _appController.ListasPresenter.ObtenerTallas());
        }

    }
}
