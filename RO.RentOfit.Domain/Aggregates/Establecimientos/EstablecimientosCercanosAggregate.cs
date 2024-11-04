
namespace RO.RentOfit.Domain.Aggregates.Establecimientos
{
    public class EstablecimientosCercanosAggregate
    {
        public string estado { get; set; }
        public string municipio { get; set; }
        public int? pagina { get; set; }
    }

    public class TodosEstablecimientosAggregate
    {
        public int usuario { get; set; }
        public int? pagina { get; set; }
    }


    public class VestimentasEstablecimientosAggregate
    {
        public int establecimiento { get; set; }
        public int usuario { get; set; }
        public int? pagina { get; set; }
    }
}
