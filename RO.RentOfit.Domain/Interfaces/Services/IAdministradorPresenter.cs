
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IAdministradorPresenter
    {

        Task<RespuestaEstablecimiento> AprobarEstablecimiento(int establecimientoId); // Método para aprobar un establecimiento

        Task<RespuestaEstablecimiento> DenegarEstablecimiento(MotivosDenegarAggregate requerimientos); // Método para denegar un establecimiento

        Task<(List<ListaDeAprobacion> Establecimientos, int TotalRegistros)> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters);
        Task<List<EstablecimientosCercanosDto>> TodosLosEstablecimientos(TodosEstablecimientosAggregate requerimientos);
        Task Alertar(MandarMsj requerimientos);
    }
}
