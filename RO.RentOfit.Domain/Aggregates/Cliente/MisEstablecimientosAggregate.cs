
namespace RO.RentOfit.Domain.Aggregates.Cliente
{
    public class MisEstablecimientosAggregate
    {
        public int usuario { get; set; }
        public int? pagina { get; set; }
    }

    public class ListaDePedidoAggregate
    {
        public int usuarioID { get; set; }
        public int mes { get; set; }
        public int ano { get; set; }
        public int? pagina { get; set; }
    }

    public class ConsultatPedidoAggregate
    {
        public int usuarioID { get; set; }
        public int? pagina { get; set; }
        public string? orden {  get; set; }
    }
}
