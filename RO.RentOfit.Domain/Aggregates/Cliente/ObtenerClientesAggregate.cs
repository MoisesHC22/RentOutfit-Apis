
namespace RO.RentOfit.Domain.Aggregates.Cliente
{
    public class ObtenerClientesAggregate
    {
        public int usuarioID { get; set; }
        public int? pagina { get; set; }
        public bool? activar { get; set; }
    }

    public class MandarMsj
    {
        public int usuarioID { get; set; }
        public string mensaje { get; set; }
    }

    public class CarritoAggregate
    {
        public int usuarioID { get; set;}
        public List<ItemsCarrito> itemsCarrito { get; set; }
    }

    public class ItemsCarrito
    {
        public int vestimentaID { get; set;}
        public int stock { get; set; }
        public DateTime? fechaPrestamo { get; set; }
    }

    public class PagoCarrito 
    {
        public int usuarioId { get; set; }
        public string paymentId { get; set; }
    }
}
