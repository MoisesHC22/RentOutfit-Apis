
namespace RO.RentOfit.Domain.DTOs.Establecimientos
{
    public class EstablecimientosCercanosDto
    {
        [Key]
        public string nombreEstablecimiento { get; set; }
        public string linkImagenEstablecimiento { get; set; }
        public string calle { get; set; }
        public string codigoPostal { get; set; }
        public string nombreEstado { get; set; }
        public string nombreMunicipio { get; set; }
    }
}
