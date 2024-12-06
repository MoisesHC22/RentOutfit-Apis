using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.RentOfit.Domain.DTOs.Establecimientos
{
    public class EstablecimientosParaAprobacionParams
    {
        public int Usuario { get; set; }
        public int? Pagina { get; set; }
        public string? Filtro { get; set; }
        public string? Orden { get; set; }
    }
}
