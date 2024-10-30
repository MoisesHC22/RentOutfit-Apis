using System.Collections.Generic;
using System.Threading.Tasks;
using RO.RentOfit.Domain.DTOs.Establecimientos;

namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IAdministradorPresenter
    {

        Task<RespuestaDB> AprobarEstablecimiento(int establecimientoId); // Método para aprobar un establecimiento

        Task<RespuestaDB> DenegarEstablecimiento(int establecimientoId); // Método para denegar un establecimiento

        Task<List<InformacionEstablecimientoDto>> ListarEstablecimientosPendientes();

        Task<List<ListaDeAprobacion>> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters);
      
    }
}
