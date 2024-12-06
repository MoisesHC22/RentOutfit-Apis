﻿
namespace RO.RentOfit.Domain.Aggregates.Cliente
{
    public class ActualizarClienteAggregate
    {
        public int clienteID { get; set; }
        public string email { get; set; }
        public string controsena { get; set; }
        public string token { get; set; }
        public string tokenValidacion { get; set; }


        public string nombreCliente { get; set; }
        public string apellidoPaterno { get; set; }
        public string apellidoMaterno { get; set; }
        public string telefono { get; set; }
        public int generoID { get; set; }


        public int estadoID { get; set; }
        public string codigoPostal { get; set; }
        public string colonia { get; set; }
        public string calle { get; set; }
        public int noInt { get; set; }
        public int noExt { get; set; }
        public string municipio { get; set; }
    }
}
