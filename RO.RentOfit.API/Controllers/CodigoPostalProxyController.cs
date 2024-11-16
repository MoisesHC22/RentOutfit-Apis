
namespace RO.RentOfit.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CodigoPostalProxyController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly ILogger<CodigoPostalProxyController> _logger;

        public CodigoPostalProxyController(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<CodigoPostalProxyController> logger)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseUrl = configuration["CodigosPostales:BaseUrl"];
            _logger = logger;
        }

        [HttpPost ("ObtenerDireccion")]
        public async Task<IActionResult> ObtenerDireccion([FromBody] CodigoPostalRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.CodigoPostal))
            {
                _logger.LogWarning("Solicitud fallida: El código postal es obligatorio.");
                return BadRequest(new { message = "El código postal es obligatorio." });
            }

            var url = $"{_baseUrl}";

            try
            {
                var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, requestContent);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Error al obtener datos del código postal. Código de estado: {StatusCode}", response.StatusCode);
                    return BadRequest(new { message = "Error al obtener datos del código postal", statusCode = response.StatusCode });
                }

                var content = await response.Content.ReadAsStringAsync();
                var dataList = JsonConvert.DeserializeObject<List<dynamic>>(content);

                if (dataList == null || dataList.Count == 0)
                {
                    return NotFound(new { message = "No se encontraron datos para el código postal proporcionado." });
                }

                var municipio = (string)dataList[0].d_mnpio;
                var estado = (string)dataList[0].d_estado;
                var asentamiento = (string)dataList[0].d_asenta;

                return Ok(new { municipio, estado, asentamiento });
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error al obtener datos del código postal");
                return BadRequest(new { message = "Error al obtener datos del código postal", error = ex.Message });
            }
        }
    }
}

