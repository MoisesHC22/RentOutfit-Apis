
namespace RO.RentOfit.Domain.Aggregates.Vendedor
{
    public class EstablecimientoAggregate
    {
        public int usuarioID { get; set; }

        public string nombreEstablecimiento { get; set; }
        
        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public int noInt { get; set; }
        public int noExt { get; set; }
        public int estadoID { get; set; }
        public string municipio { get; set; }
        public IFormFile imagen { get; set; }
    }
}
