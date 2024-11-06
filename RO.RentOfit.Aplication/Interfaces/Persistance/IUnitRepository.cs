
using RO.RentOfit.Domain.Interfaces.Infrastructure;

namespace RO.RentOfit.Aplication.Interfaces.Persistance
{
    public interface IUnitRepository
    {
        ValueTask<bool> Complete();
        bool HasChanges();
        IClienteInfraestructure clienteInfraestructure { get; }
        IListasInfraestructure listasInfraestructure { get; }
        IVendedorInfraestructure vendedorInfraestructure { get; }
        IRecuperarContrasenaInfraestructure recuperarContrasenaInfraestructure { get; }
        IAdministradorInfraestructure administradorInfraestructure { get; }
    }
}
