
using NPOI.OpenXmlFormats.Dml;

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

        [HttpPost("MisEstablecimientos")]
        public async ValueTask<IActionResult> MisEstablecimientos(MisEstablecimientosAggregate requerimientos)
        {
            return Ok(await _appController.vendedorPresenter.MisEstablecimientos(requerimientos));
        }


        [HttpPost("GenerarPdfEstablecimientos")]
        public async Task<IActionResult> GenerarPdfEstablecimientos([FromBody] int usuarioID) 
        {
            var establecimientos = await _appController.vendedorPresenter.MisEstablecimientos(new MisEstablecimientosAggregate { usuario = usuarioID });

            if (establecimientos == null || !establecimientos.Any()) 
            {
                return NotFound("No se encontraron establecimientos.");
            }

            using (var memoryStream = new MemoryStream()) 
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                document.Add(new Paragraph("Tus Establecimientos").SetBold().SetFontSize(20).SetTextAlignment(TextAlignment.CENTER));
                
                document.Add(new Paragraph("\n"));
                document.Add(new Paragraph("\n"));


                foreach (var establecimiento in establecimientos) 
                {
                    document.Add(new Paragraph($"{establecimiento.nombreEstablecimiento}").SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("\n"));

                    var vestimentas = await _appController.ClientePresenter.VestimentasDeEstablecimientos(new VestimentasEstablecimientosAggregate
                    {
                        establecimiento = establecimiento.establecimientosID,
                        usuario = usuarioID
                    });

                    if (vestimentas != null && vestimentas.Any())
                    {
                        float[] Columas = { 6, 4 };
                        Table tabla = new Table(UnitValue.CreatePercentArray(Columas)).UseAllAvailableWidth();

                        tabla.AddHeaderCell(new Cell().Add(new Paragraph("Vestimenta").SetBold()));
                        tabla.AddHeaderCell(new Cell().Add(new Paragraph("Precio por día").SetBold()));

                        foreach (var vestimenta in vestimentas)
                        {
                            tabla.AddCell(new Paragraph(vestimenta.nombrePrenda));
                            tabla.AddCell(new Paragraph(vestimenta.precioPorDia.ToString("C")));
                        }

                        document.Add(tabla);

                    } else {
                        document.Add(new Paragraph("No cuentas con vestimentas para este establecimiento."));
                    }

                    document.Add(new Paragraph("\n"));
                    document.Add(new LineSeparator(new SolidLine()));
                    document.Add(new Paragraph("\n"));
                    document.Add(new Paragraph("\n"));

                }
                document.Close();

                var pdfBytes = memoryStream.ToArray();
                return File(pdfBytes, "application/pdf", "establecimientos.pdf");
            }
        }


        [HttpPost("ConsultarPedidos")]
        public async ValueTask<IActionResult> ConsultarPedidos(ConsultatPedidoAggregate requerimientos)
        {
            return Ok(await _appController.vendedorPresenter.consultarPedidos(requerimientos));
        }

    }
}
