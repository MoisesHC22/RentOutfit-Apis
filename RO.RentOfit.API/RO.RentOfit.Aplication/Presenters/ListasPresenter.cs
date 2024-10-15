
using RO.RentOfit.Domain.DTOs.Tallas;

namespace RO.RentOfit.Aplication.Presenters
{
    public class ListasPresenter : IListasPresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public ListasPresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<List<GeneroDto>> ObtenerGeneros()
        {
            return await _unitRepository.listasInfraestructure.ObtenerGeneros();
        }

        public async Task<List<EstadosDto>> ObtenerEstados()
        {
            return await _unitRepository.listasInfraestructure.ObtenerEstados();
        }

        public async Task<List<MunicipiosDto>> ObtenerMunicipios(int estadoID)
        {
            return await _unitRepository.listasInfraestructure.ObtenerMunicipios(estadoID);
        }

        public async Task<List<TallasDto>> ObtenerTallas()
        {
            return await _unitRepository.listasInfraestructure.ObtenerTallas();
        }

    }
}
