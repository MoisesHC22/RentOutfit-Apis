
namespace RO.RentOfit.Domain.DTOs.Cliente
{
    public class IniciarSesionDto
    {
        [Key]
        public int detalleRolID { get; set; }
        public string nombreCliente { get; set; }
        public string linkImagenPerfil { get; set; }
        public string nombreEstado { get; set; }
        public string municipio { get; set; }

    }
}
