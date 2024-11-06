
namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IListasInfraestructure
    {
        Task<List<EstadosDto>> ObtenerEstados();
        Task<List<EstilosDto>> ObtenerEstilos();
        Task<List<GeneroDto>> ObtenerGeneros();
        Task<List<MunicipiosDto>> ObtenerMunicipios(int estadoID);
        Task<List<TallasDto>> ObtenerTallas();
    }
}
