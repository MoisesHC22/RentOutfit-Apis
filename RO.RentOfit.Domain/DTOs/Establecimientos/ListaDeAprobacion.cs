﻿
namespace RO.RentOfit.Domain.DTOs.Establecimientos
{
    public class ListaDeAprobacion
    {
        [Key]
        public int establecimientosID { get; set; }
        public string nombreEstablecimiento { get; set; }
        public string linkImagenEstablecimiento { get; set; }
    }
}