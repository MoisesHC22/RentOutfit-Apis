
using RO.RentOfit.Domain.DTOs.Cliente;

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

        public async Task<RespuestaDB> RegistrarCliente(RegistrarClienteDto registro)
        {
            return await _unitRepository.clienteInfraestructure.RegistrarCliente(registro);
        }
    }
}
