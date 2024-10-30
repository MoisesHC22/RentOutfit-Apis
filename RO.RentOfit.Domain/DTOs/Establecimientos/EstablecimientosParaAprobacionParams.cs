using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RO.RentOfit.Domain.DTOs.Establecimientos
{
    public class EstablecimientosParaAprobacionParams
    {
        public int? Usuario { get; set; } = null;
        public int Pagina { get; set; } = 1;
    }
}
