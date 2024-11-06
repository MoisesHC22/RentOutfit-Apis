
namespace RO.RentOfit.Domain.DTOs.Establecimientos
{
    public class InformacionEstablecimientoDto
    {
        [Key]
        public int establecimientosID { get; set; }
        public string nombreEstablecimiento { get; set;}
        public string linkImagenEstablecimiento { get; set; }
        public string nombreEstado { get; set; }
        public string municipio { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public string noInt { get; set; }
        public string noExt { get; set; }
        public string codigoPostal { get; set; }
        public int usuarioID { get; set; }
        public string linkImagenPerfil { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }

    }
}
