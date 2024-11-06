
namespace RO.RentOfit.Domain.Aggregates.Cliente
{
    public class RegistrarClienteAggregate
    {

        public string email { get; set; }
        public string contrasena { get; set; }



        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string telefono { get; set; }
        public int generoID { get; set; }
        public IFormFile imagen { get; set; }

    }
}
