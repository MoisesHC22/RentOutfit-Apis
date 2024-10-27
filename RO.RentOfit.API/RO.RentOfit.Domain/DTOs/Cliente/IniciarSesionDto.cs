
namespace RO.RentOfit.Domain.DTOs.Cliente
{
    public class IniciarSesionDto
    {
        [Key]
        public int detalleRolID { get; set; }
        public int usuarioID { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string linkImagenPerfil { get; set; }

    }
}
