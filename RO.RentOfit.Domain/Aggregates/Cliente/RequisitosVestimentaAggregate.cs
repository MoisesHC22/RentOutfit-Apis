
namespace RO.RentOfit.Domain.Aggregates.Cliente
{
    public class RequisitosVestimentaAggregate
    {
        public string estado { get; set; }
        public string municipio { get; set; }
        public int? pagina { get; set; }
        public string? filtro { get; set; }
        public int? categoria { get; set; }
        public int? talla { get; set; }
    }
}
