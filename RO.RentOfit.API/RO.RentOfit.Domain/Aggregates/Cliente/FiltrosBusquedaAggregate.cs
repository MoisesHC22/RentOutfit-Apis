
namespace RO.RentOfit.Domain.Aggregates.Cliente
{
    public class FiltrosBusquedaAggregate
    {
        public string? nombre { get; set; }
        public string? estilo { get; set; }
        public string? talla { get; set; }
        public string? establecimiento { get; set; }
        public string? estado { get; set; }
        public string? municipio { get; set; }
        public int? usuario { get; set; }
        public int? pagina { get; set; }

    }
}
