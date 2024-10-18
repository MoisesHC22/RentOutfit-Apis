
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VendedorController : ApiController
    {
        private readonly IConfiguration _configuration;

        public  VendedorController(IApiController appController, IConfiguration configuration) : base(appController)
        {
            _configuration = configuration;
        }



        [HttpPost("DarDeAltaUnVendedor")]
        public async ValueTask<IActionResult> DarDeAltaUnVendedor([FromBody] int usuarioID)
        {
            return Ok(await _appController.vendedorPresenter.DarDeAltaUnVendedor(usuarioID));
        }



        [HttpPost("DarDeAltaEstablecimiento")]
        public async ValueTask<IActionResult> DarDeAltaEstablecimiento(EstablecimientoAggregate requerimiento) 
        {
            return Ok(await _appController.vendedorPresenter.DarDeAltaEstablecimiento(requerimiento));
        }



        [HttpPost("RegistrarVestimenta")]
        public async ValueTask<IActionResult> RegistrarVestimenta(VestimentaAggregate registro)
        {
            IFormFile[]  imagenes = { registro.imagen1, registro.imagen2, registro.imagen3, registro.imagen4 };
            return Ok(await _appController.vendedorPresenter.RegistrarVestimentas(registro, imagenes));
        }

    }
}
