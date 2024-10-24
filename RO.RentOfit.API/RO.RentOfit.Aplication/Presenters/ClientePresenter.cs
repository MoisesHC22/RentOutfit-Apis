
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



        public async Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro)
        {
            return await _unitRepository.clienteInfraestructure.RegistrarCliente(registro);
        }



        public async Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento) 
        {
            return await _unitRepository.clienteInfraestructure.IniciarSesion(requerimiento);
        }


        public async Task<InformacionVestimentaDto> InformacionVestimenta(int vestimenta)
        {
            return await _unitRepository.clienteInfraestructure.InformacionVestimenta(vestimenta);
        }

        public async Task<List<ListaVestimentasDto>> MostrarVestimentas(RequisitosVestimentaAggregate requerimientos)
        {
            return await _unitRepository.clienteInfraestructure.MostrarVestimentas(requerimientos);
        }

        public async Task<List<EstablecimientosCercanosDto>> EstablecimientosCercanos(EstablecimientosCercanosAggregate requerimientos)
        {
            return await _unitRepository.clienteInfraestructure.EstablecimientosCercanos(requerimientos);
        }
    }
}
