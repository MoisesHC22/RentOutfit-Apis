
namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructure
    {
        Task<List<EstablecimientosCercanosDto>> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos);
        Task<InformacionEstablecimientoDto> InformacionEstablecimiento(int establecimiento);
        Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta);
        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
        Task<List<ListaVestimentasDto>> MostrarVestimentas(RequisitosVestimentaAggregate requerimientos);
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro);
        Task<List<VestimentasEstablecimientosDto>> VestimentasDeEstablecimientos(VestimentasEstablecimientosAggregate requerimientos);
    }
}
