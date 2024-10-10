
namespace RO.RentOfit.Aplication.Interfaces.Persistance
{
    public interface IUnitRepository
    {
        ValueTask<bool> Complete();
        bool HasChanges();
        IClienteInfraestructure clienteInfraestructure { get; }
        IListasInfraestructure listasInfraestructure { get; }
    }
}
