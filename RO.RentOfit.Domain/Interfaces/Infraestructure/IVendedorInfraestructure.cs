

namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IVendedorInfraestructure
    {
        Task<RetornoEstablecimientoDto> DarDeAltaEstablecimiento(EstablecimientoAggregate registro);
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
        Task<RespuestaDB> RegistrarVestimentas(VestimentaAggregate registro, IFormFile[] Imagenes);
        Task<List<MisEstablecimientosDto>> MisEstablecimientos(MisEstablecimientosAggregate requerimientos);
        Task<List<ConsultarPedidosDto>> consultarPedidos(ConsultatPedidoAggregate requerimientos);
    }
}
