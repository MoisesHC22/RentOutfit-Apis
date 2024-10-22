
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


        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public string noInt { get; set; }
        public string noExt { get; set; }
        public string municipio { get; set; }


        public string estado { get; set; }
        public string genero { get; set; }
    }
}
