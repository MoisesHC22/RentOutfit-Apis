
namespace RO.RentOfit.Domain.DTOs.Establecimientos
{
    public class RetornoEstablecimientoDto
    {
        [Key]
        public int tipoError { get; set; }
        public string mensaje { get; set; }
        public int? EstablecimientoID { get; set; }
    }
}
