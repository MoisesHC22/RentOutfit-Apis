
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
        public async ValueTask<IActionResult> RegistrarCliente([FromForm] RegistrarClienteAggregate registro, IFormFile imagenPerfil)
        {
            return Ok( await _appController.ClientePresenter.RegistrarCliente(registro, imagenPerfil));
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
                    new Claim(ClaimTypes.Name, cliente.nombreCliente),
                    new Claim(ClaimTypes.Name, cliente.nombreEstado),
                    new Claim(ClaimTypes.Name, cliente.municipio),
                    new Claim(ClaimTypes.Name, cliente.linkImagenPerfil),
                    new Claim(ClaimTypes.Role, (cliente.detalleRolID).ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }


    }
}
