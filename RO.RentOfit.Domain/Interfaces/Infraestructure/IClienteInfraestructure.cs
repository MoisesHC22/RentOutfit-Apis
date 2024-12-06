﻿
namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructure
    {
        Task<List<ItemsCarrito>> CargarCarrito(int usuarioID);
        Task<List<EstablecimientosCercanosDto>> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos);
        Task GuardarCarrito(CarritoAggregate requerimientos);
        Task<InformacionDeUnaVestimenta> InformacionDeUnaSolaVestimenta(int vestimentaID);
        Task<InformacionEstablecimientoDto> InformacionEstablecimiento(int establecimiento);
        Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta);
        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
        Task<List<ListaPedido>> ListaDeRentas(ListaDePedidoAggregate requerimientos);
        Task<List<ListaVestimentasDto>> MostrarVestimentas(RequisitosVestimentaAggregate requerimientos);
        Task<List<ClienteDto>> ObtenerCliente(ObtenerClientesAggregate requerimientos);
        Task<ClienteDto> PagoExitoso(int usuarioID);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro);
        Task<List<VestimentasEstablecimientosDto>> VestimentasDeEstablecimientos(VestimentasEstablecimientosAggregate requerimientos);
    }
}
