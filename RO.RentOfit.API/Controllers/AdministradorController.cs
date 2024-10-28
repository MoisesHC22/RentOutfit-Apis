
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




    }
}
