
namespace RO.RentOfit.Domain.DTOs.Vestimenta
{
    public class ListaVestimentasDto
    {
        [Key]
        public int vestimentaID { get; set; }
        public string nombrePrenda { get; set; }
        public decimal precioPorDia { get; set; }
        public string imagen1 { get; set; }
        public string nombreTalla { get; set; }
        public string nombreEstilo { get; set; }
        public string nombreEstablecimiento { get; set; }
    }
}
