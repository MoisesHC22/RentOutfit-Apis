using System.Collections.Generic;
using System.Threading.Tasks;
using RO.RentOfit.Domain.DTOs.Establecimientos;

namespace RO.RentOfit.Domain.Interfaces.Infrastructure
{
    public interface IAdministradorInfraestructure
    {
        // Método para consultar establecimientos pendientes de aprobación

        // Método para aprobar un establecimiento
        Task<RespuestaDB> AprobarEstablecimiento(int establecimientoId);

        // Método para denegar un establecimiento con una razón de rechazo
        Task<RespuestaDB> DenegarEstablecimiento(int establecimientoId);

        // Método para listar todos los establecimientos pendientes de revisión
        Task<List<InformacionEstablecimientoDto>> ListarEstablecimientosPendientes();
        Task<List<ListaDeAprobacion>> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters);
    }
}
