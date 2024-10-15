
namespace RO.RentOfit.Aplication.Presenters
{
    public class ClientePresenter : IClientePresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public ClientePresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> ObtenerCliente(int usuarioID)
        {
            return await _unitRepository.clienteInfraestructure.ObtenerCliente(usuarioID);
        }

        public async Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro, IFormFile Imagen)
        {
            return await _unitRepository.clienteInfraestructure.RegistrarCliente(registro, Imagen);
        }

        public async Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento) 
        {
            return await _unitRepository.clienteInfraestructure.IniciarSesion(requerimiento);
        }
        
    }
}
