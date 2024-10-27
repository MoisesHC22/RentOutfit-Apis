
namespace RO.RentOfit.Domain.DTOs.Vestimenta
{
    public class InformacionVestimentaDto
    {
        [Key]
        public int vestimentaID { get; set; }
        public string nombrePrenda { get; set; }
        public decimal precioPorDia { get; set; }
        public int stock { get; set; }
        public DateTime fechaDePublicacion { get; set; }
        public bool vestimentaEstatus { get; set; }
        public string descripcion { get; set; }
        
        public string imagen1 { get; set; }
        public string imagen2 { get; set; }
        public string imagen3 { get; set; }
        public string imagen4 { get; set; }

        public string nombreTalla { get; set; }
        public string nombreEstilo { get; set; }

    }
}
