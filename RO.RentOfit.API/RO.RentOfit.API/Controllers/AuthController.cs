/// Developer : Bryan Hernadez Garcia
/// Creation Date : 02/10/2024
/// Creation Description:Controller
/// Update Date : --
/// Update Description : --
///CopyRight: Nombre proyecto

namespace RO.RentOfit.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ApiController
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="appController"></param>
    public AuthController(IApiController appController) : base(appController)
    {

    }

}
