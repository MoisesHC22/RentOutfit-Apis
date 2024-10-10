using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.RentOfit.Domain.DTOs.Cliente
{
    public class ClienteDto
    {
        [Key]
        public int clienteID { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }


        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string linkImagenPerfil { get; set; }
        public string telefono { get; set; }


        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public int noInt { get; set; }
        public int noExt { get; set; }
        public string municipio { get; set; }


        public string estado { get; set; }
        public string genero { get; set; }
    }
}
