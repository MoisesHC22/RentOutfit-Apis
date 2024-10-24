
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IClientePresenter
    {
        Task<List<EstablecimientosCercanosDto>> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos);
        Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta);

        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        Task<List<ListaVestimentasDto>> MostrarVestimentas(RequisitosVestimentaAggregate requerimientos);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro);

        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
    }
}
