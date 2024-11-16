
namespace RO.RentOfit.Aplication.Presenters
{
    public class RecuperarContrasenaPresenter : IRecuperarContrasenaPresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public RecuperarContrasenaPresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }



        public async Task<RecuperarContrasenaDto> ObtenerToken(string email)
        {
            return await _unitRepository.recuperarContrasenaInfraestructure.ObtenerToken(email);
        }



        public async Task<RespuestaValidarToken> ValidarToken(ValidarToken requerimientos)
        {
            return await _unitRepository.recuperarContrasenaInfraestructure.ValidarToken(requerimientos);
        }



        public async Task<RespuestaDB> ActualizarContrasena(ActualizarContrasena Requerimientos)
        {
            return await _unitRepository.recuperarContrasenaInfraestructure.ActualizarContrasena(Requerimientos);
        }

    }
}
