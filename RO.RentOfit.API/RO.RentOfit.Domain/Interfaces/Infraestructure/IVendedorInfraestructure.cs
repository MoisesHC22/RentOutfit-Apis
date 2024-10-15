
using RO.RentOfit.Domain.Aggregates.Vendedor;

namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IVendedorInfraestructure
    {
        Task<RespuestaDB> DarDeAltaEstablecimiento(EstablecimientoAggregate registro);
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
    }
}
