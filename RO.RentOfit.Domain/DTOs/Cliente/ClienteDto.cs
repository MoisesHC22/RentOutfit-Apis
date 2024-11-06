
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
}
