
namespace RO.RentOfit.Aplication.Presenters
{
    public class VendedorPresenter : IVendedorPresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public VendedorPresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }



        public async Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID)
        {
            return await _unitRepository.vendedorInfraestructure.DarDeAltaUnVendedor(usuarioID);
        }



        public async Task<RetornoEstablecimientoDto> DarDeAltaEstablecimiento(EstablecimientoAggregate registro)
        {
            return await _unitRepository.vendedorInfraestructure.DarDeAltaEstablecimiento(registro);
        }



        public async Task<RespuestaDB> RegistrarVestimentas(VestimentaAggregate registro, IFormFile[] Imagenes)
        {
            return await _unitRepository.vendedorInfraestructure.RegistrarVestimentas(registro, Imagenes);
        }
    }
}
