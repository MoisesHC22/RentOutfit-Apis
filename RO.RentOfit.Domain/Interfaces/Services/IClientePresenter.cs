﻿
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IClientePresenter
    {
        Task<List<EstablecimientosCercanosDto>> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos);
        Task<InformacionEstablecimientoDto> InformacionEstablecimiento(int establecimiento);
        Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta);
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        Task<List<ListaVestimentasDto>> MostrarVestimentas(RequisitosVestimentaAggregate requerimientos);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro);
        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);

        Task<List<VestimentasEstablecimientosDto>> VestimentasDeEstablecimientos(VestimentasEstablecimientosAggregate requerimientos);
    }
}