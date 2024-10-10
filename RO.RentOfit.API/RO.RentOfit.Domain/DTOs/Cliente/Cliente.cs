
namespace RO.RentOfit.Domain.DTOs.Cliente
{
    public class Cliente
    {
        [Key]
        public int clienteID { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string linkImagenPerfil { get; set; }
        public int usuarioID { get; set; }
        public string telefono { get; set; }
        public int direccionID { get; set; }
        public string generoID { get; set; }
        public DateTime ultimaModificacionCliente { get; set; }
    }
}
