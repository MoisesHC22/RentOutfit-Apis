
namespace RO.RentOfit.Domain.Aggregates.Vendedor
{
    public class VestimentaAggregate
    {
        public int usuarioID { get; set; }
        public int establecimientoID { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public double precio { get; set; }
        public int tallaID { get; set; }
        public int estiloID { get; set; }
        public string descripcion { get; set; }

        public IFormFile imagen1 { get; set; }
        public IFormFile? imagen2 { get; set; }
        public IFormFile? imagen3 { get; set; }
        public IFormFile? imagen4 { get; set; }


    }
}
