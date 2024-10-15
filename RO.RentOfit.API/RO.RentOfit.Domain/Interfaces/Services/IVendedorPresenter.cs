
using RO.RentOfit.Domain.Aggregates.Vendedor;

namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IVendedorPresenter
    {
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
        Task<RespuestaDB> DarDeAltaEstablecimiento(EstablecimientoAggregate registro);
    }
}
