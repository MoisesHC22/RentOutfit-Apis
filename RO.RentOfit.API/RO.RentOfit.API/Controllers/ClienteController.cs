
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ApiController
    {
        private readonly IConfiguration _configuration;

        public ClienteController(IApiController appController, IConfiguration configuration) : base(appController) 
        {
            _configuration = configuration;
        }



        [HttpPost("ObtenerCliente")]
        public async ValueTask<IActionResult> ObtenerCliente([FromBody] int usuarioID)
        {
            var clientes = await _appController.ClientePresenter.ObtenerCliente(usuarioID);

            if (clientes == null || !clientes.Any())
            {
                return Unauthorized();
            }

            var clientesJson = JsonConvert.SerializeObject(clientes);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                 new Claim("Clientes", clientesJson)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }


        [HttpPost("RegistrarCliente")]
        public async ValueTask<IActionResult> RegistrarCliente(RegistrarClienteAggregate registro)
        {
            return Ok( await _appController.ClientePresenter.RegistrarCliente(registro));
        }


        [HttpPost("IniciarSesion")]
        public async ValueTask<IActionResult> IniciarSesion([FromBody] IniciarSesionAggregate requerimiento)
        {
            var cliente = await _appController.ClientePresenter.IniciarSesion(requerimiento);

            if (cliente == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, (cliente.detalleRolID).ToString()),
                    new Claim("usuario", (cliente.usuarioID).ToString()),
                    new Claim("nombre", cliente.nombreCliente + " " + cliente.apellidoPaterno + " " +cliente.apellidoMaterno),
                    new Claim("imagen", cliente.linkImagenPerfil)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }


        [HttpPost ("InformacionVestimenta")]
        public async ValueTask<IActionResult> InformacionVestimenta([FromBody] int vestimenta) 
        {
            return Ok(await _appController.ClientePresenter.InformacionVestimenta(vestimenta));
        }


        [HttpPost("EstablecimientosCercanos")]
        public async ValueTask<IActionResult> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos) 
        {
            return Ok(await _appController.ClientePresenter.EstablecimientosCercanos(requerimientos));
        }


        [HttpPost("MostrarVestimentas")]
        public async ValueTask<IActionResult> MostrarVestimentas(RequisitosVestimentaAggregate requerimientos)
        {
            return Ok(await _appController.ClientePresenter.MostrarVestimentas(requerimientos));
        }



        [HttpPost("InformacionEstablecimiento")]
        public async ValueTask<IActionResult> InformacionEstablecimiento([FromBody] int establecimiento)
        {
            return Ok(await _appController.ClientePresenter.InformacionEstablecimiento(establecimiento));
        }


    }
}
