
namespace RO.RentOfit.Aplication.Presenters
{
    public class AdministradorPresenter : IAdministradorPresenter
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IMapper _mapper;

        public AdministradorPresenter(IUnitRepository unitRepository, IMapper mapper)
        {
            _unitRepository = unitRepository;
            _mapper = mapper;
        }

        // Método para consultar establecimientos para aprobación
        public async Task<(List<ListaDeAprobacion> Establecimientos, int TotalRegistros)> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters)
        {
            // Aquí no es necesario manejar excepciones, ya que el repositorio lo hace
            return await _unitRepository.administradorInfraestructure.ConsultarEstablecimientosParaAprobacion(parameters);
        }

        // Método para aprobar un establecimiento específico
        public async Task<RespuestaEstablecimiento> AprobarEstablecimiento(int establecimientoId)
        {
            return await _unitRepository.administradorInfraestructure.AprobarEstablecimiento(establecimientoId);
        }

        // Método para rechazar un establecimiento con una razón específica
        public async Task<RespuestaEstablecimiento> DenegarEstablecimiento(MotivosDenegarAggregate requerimientos)
        {
            return await _unitRepository.administradorInfraestructure.DenegarEstablecimiento(requerimientos);
        }

        // Método para obtener la información de un cliente

        public async Task<List<EstablecimientosCercanosDto>> TodosLosEstablecimientos(TodosEstablecimientosAggregate requerimientos) 
        {
            return await _unitRepository.administradorInfraestructure.TodosLosEstablecimientos(requerimientos);
        }

        public async Task Alertar(MandarMsj requerimientos) 
        {
            await _unitRepository.administradorInfraestructure.Alertar(requerimientos);
        }

    }
}
