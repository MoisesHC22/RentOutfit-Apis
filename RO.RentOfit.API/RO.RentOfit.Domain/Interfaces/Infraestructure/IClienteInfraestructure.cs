
using RO.RentOfit.Domain.DTOs.Vestimenta;

namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructure
    {
        Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta);
        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
        //Task<List<ListaVestimentasDto>> MostrarVestimentas(FiltrosBusquedaAggregate requerimientos);
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro);
    }
}
