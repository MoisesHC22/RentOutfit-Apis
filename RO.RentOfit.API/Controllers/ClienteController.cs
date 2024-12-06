using MercadoPago.Client.Preference;
using MercadoPago.Resource.Preference;
using MercadoPago.Config;
using iText.StyledXmlParser.Node;
using Microsoft.Extensions.Azure;
using Azure.Core;

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
        public async ValueTask<IActionResult> ObtenerCliente(ObtenerClientesAggregate requerimientos)
        {
            var clientes = await _appController.ClientePresenter.ObtenerCliente(requerimientos);

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


        [HttpPost("VestimentasDeEstablecimientos")]
        public async ValueTask<IActionResult> VestimentasDeEstablecimientos(VestimentasEstablecimientosAggregate requerimientos)
        {
            return Ok(await _appController.ClientePresenter.VestimentasDeEstablecimientos(requerimientos));
        }



        [HttpPost("ModificarCarrito")]
        public async Task<IActionResult> ModificarCarrito(CarritoAggregate requerimientos)
        {
            await _appController.ClientePresenter.GuardarCarrito(requerimientos);
            return Ok(new { mensaje = "Se cumplio con exito" });
        }


        [HttpPost("CargarCarrito")]
        public async Task<IActionResult> CargarCarrito([FromBody] int usuarioID)
        {
            return Ok(await _appController.ClientePresenter.CargarCarrito(usuarioID));
        }


        [HttpPost("GenerarTokenMercadoPago")]
        public async Task<IActionResult> GenerarTokenMercadoPago([FromBody] int usurioID) 
        {

            MercadoPagoConfig.AccessToken = "TEST-1228696711826746-111815-2cc6c73a2c5c8ae98848122680486337-1414719981";

            var carrito = await _appController.ClientePresenter.CargarCarrito(usurioID);

            if (carrito == null || !carrito.Any())
            {
                return BadRequest("El carrito está vacío o no existe.");
            }

            var items = new List<PreferenceItemRequest>();

            foreach (var item in carrito)
            {
                var vestimenta = await _appController.ClientePresenter.InformacionDeUnaSolaVestimenta(item.vestimentaID);

                if (vestimenta != null)
                {
                    items.Add(new PreferenceItemRequest
                    {
                        Title = vestimenta.nombrePrenda,
                        Quantity = item.stock,
                        CurrencyId = "MXN",
                        UnitPrice = vestimenta.precioPorDia
                    });
                }
            }

            if (!items.Any())
            {
                return BadRequest("No se pudo generar la lista de ítems para Mercado Pago.");
            }

            var cliente = new PreferenceClient();

            var request = new PreferenceRequest
            {
                Items = items,
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://rent-outfit-web.vercel.app/Cliente/home",
                    Failure = "https://rent-outfit-web.vercel.app/Cliente/carritoDeCompras",
                    Pending = "https://rent-outfit-web.vercel.app/Cliente/carritoDeCompras"
                },
                AutoReturn = "approved"
            };

            Preference preference = await cliente.CreateAsync(request);

            if (preference != null)
            {
                Console.WriteLine(preference.Id);
                return Ok(new { preferenceId = preference.Id });
            }
            else
            {
                return BadRequest("No se pudo generar la preferencia.");
            }
        }



        [HttpPost("GenerarTokenMercadoPagoMovil")]
        public async Task<IActionResult> GenerarTokenMercadoPagoMovil([FromBody] int usurioID)
        {

            MercadoPagoConfig.AccessToken = "TEST-1228696711826746-111815-2cc6c73a2c5c8ae98848122680486337-1414719981";

            var carrito = await _appController.ClientePresenter.CargarCarrito(usurioID);

            if (carrito == null || !carrito.Any())
            {
                return BadRequest("El carrito está vacío o no existe.");
            }

            var items = new List<PreferenceItemRequest>();

            foreach (var item in carrito)
            {
                var vestimenta = await _appController.ClientePresenter.InformacionDeUnaSolaVestimenta(item.vestimentaID);

                if (vestimenta != null)
                {
                    items.Add(new PreferenceItemRequest
                    {
                        Title = vestimenta.nombrePrenda,
                        Quantity = item.stock,
                        CurrencyId = "MXN",
                        UnitPrice = vestimenta.precioPorDia
                    });
                }
            }

            if (!items.Any())
            {
                return BadRequest("No se pudo generar la lista de ítems para Mercado Pago.");
            }

            var cliente = new PreferenceClient();

            var request = new PreferenceRequest
            {
                Items = items,
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "myapp://checkout-success",
                    Failure = "myapp://checkout-failure",
                    Pending = "myapp://checkout-pending"
                },
                AutoReturn = "approved"
            };

            Preference preference = await cliente.CreateAsync(request);

            if (preference != null)
            {
                Console.WriteLine(preference.Id);
                return Ok(new { preferenceId = preference.Id });
            }
            else
            {
                return BadRequest("No se pudo generar la preferencia.");
            }
        }


        [HttpPost("GuardarPago")]
        public async Task<IActionResult> GuardarPago(PagoCarrito requerimientos)
        {
            try 
            {
                var accessToken = "TEST-1228696711826746-111815-2cc6c73a2c5c8ae98848122680486337-1414719981";

                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = $"https://api.mercadopago.com/v1/payments/{requerimientos.paymentId}";

                    httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent);

                        string paymentStatus = jsonResponse.status;

                        if (paymentStatus == "approved")
                        {
                            await _appController.ClientePresenter.PagoExitoso(requerimientos.usuarioId);

                            return Ok(new
                            {
                                message = "Pago procesado correctamente.",
                                status = paymentStatus
                            });
                        }
                        else 
                        {
                            return Ok(new
                            {
                                message = "El pago aún no está aprobado.",
                                status = paymentStatus
                            });
                        }
                    }
                    else
                    {
                        // Maneja errores en la respuesta
                        string errorContent = await response.Content.ReadAsStringAsync();
                        return StatusCode((int)response.StatusCode, new
                        {
                            message = "Error al consultar el estado del pago",
                            details = errorContent
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error interno al consultar el estado del pago",
                    details = ex.Message
                });
            }
        }


        [HttpPost("ListaDeRentas")]
        public async ValueTask<IActionResult> ListaDeRentas(ListaDePedidoAggregate requerimientos)
        {
            return Ok(await _appController.ClientePresenter.ListaDeRentas(requerimientos));
        }

    }
}
