
namespace RO.RentOfit.Domain.Interfaces.Infrastructure
{
    public interface IAdministradorInfraestructure
    {
        // Método para consultar establecimientos pendientes de aprobación

        // Método para aprobar un establecimiento
        Task<RespuestaEstablecimiento> AprobarEstablecimiento(int establecimientoId);

        // Método para denegar un establecimiento con una razón de rechazo
        Task<RespuestaEstablecimiento> DenegarEstablecimiento(MotivosDenegarAggregate requerimientos);

        // Método para listar todos los establecimientos pendientes de revisión
        Task<(List<ListaDeAprobacion> Establecimientos, int TotalRegistros)> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters);
        Task<List<EstablecimientosCercanosDto>> TodosLosEstablecimientos(TodosEstablecimientosAggregate requerimientos);
        Task Alertar(MandarMsj requerimientos);
    }
}
