
namespace RO.RentOfit.Domain.DTOs.Cliente
{
    public class ClienteDto
    {
        [Key]
        public int usuarioID { get; set; }
        public string email { get; set; }

        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string linkImagenPerfil { get; set; }
        public string telefono { get; set; }
        public string genero { get; set; }
    }

    public class ListaPedido
    {
        [Key]
        public int detalleVentaID { get; set; }
        public string imagen1 { get; set; }
        public string nombrePrenda { get; set; }
        public DateOnly fechaPrestamo { get; set; }
        public int totalRegistros { get; set; }
    }

    public class ConsultarPedidosDto
    {
        [Key]
        public int pedidoID { get; set; }
        public string nombreEstablecimiento { get; set; }
        public DateTime ultimaModifiacionPedido { get; set; }
        public decimal total { get; set; }
        public bool pedidosEstatus {  get; set; }
        public int totalRegistros { get; set; }
    }


}
