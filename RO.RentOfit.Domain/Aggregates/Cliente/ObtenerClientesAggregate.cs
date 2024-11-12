
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
}
