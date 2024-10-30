using System.Collections.Generic;
using System.Threading.Tasks;
using RO.RentOfit.Domain.DTOs.Establecimientos;
using RO.RentOfit.Domain.Interfaces.Services;
using RO.RentOfit.Domain.Interfaces.Infrastructure;
using AutoMapper;

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
        public async Task<List<ListaDeAprobacion>> ConsultarEstablecimientosParaAprobacion(EstablecimientosParaAprobacionParams parameters)
        {
            // Aquí no es necesario manejar excepciones, ya que el repositorio lo hace
            return await _unitRepository.administradorInfraestructure.ConsultarEstablecimientosParaAprobacion(parameters);
        }

        // Método para aprobar un establecimiento específico
        public async Task<RespuestaDB> AprobarEstablecimiento(int establecimientoId)
        {
            return await _unitRepository.administradorInfraestructure.AprobarEstablecimiento(establecimientoId);
        }

        // Método para rechazar un establecimiento con una razón específica
        public async Task<RespuestaDB> DenegarEstablecimiento(int establecimientoId)
        {
            return await _unitRepository.administradorInfraestructure.DenegarEstablecimiento(establecimientoId);
        }

        // Método para obtener la información de un cliente
       
        // Método para listar todos los establecimientos pendientes de revisión
        public async Task<List<InformacionEstablecimientoDto>> ListarEstablecimientosPendientes()
        {
            return await _unitRepository.administradorInfraestructure.ListarEstablecimientosPendientes();
        }
    }
}
