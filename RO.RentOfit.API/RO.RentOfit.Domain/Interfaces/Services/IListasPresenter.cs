
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IListasPresenter
    {
        Task<List<GeneroDto>> ObtenerGeneros();

        Task<List<EstadosDto>> ObtenerEstados();

        Task<List<MunicipiosDto>> ObtenerMunicipios(int estadoID);
    }
}
