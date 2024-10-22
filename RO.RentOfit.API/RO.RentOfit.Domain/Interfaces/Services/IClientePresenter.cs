
using RO.RentOfit.Domain.DTOs.Vestimenta;

namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IClientePresenter
    {
        Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta);

        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        //Task<List<ListaVestimentasDto>> MostrarVestimentas(FiltrosBusquedaAggregate requerimientos);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro);

        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
    }
}
