﻿
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



        public async Task<List<ClienteDto>> ObtenerCliente(ObtenerClientesAggregate requerimientos)
        {
            return await _unitRepository.clienteInfraestructure.ObtenerCliente(requerimientos);
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

        public async Task<InformacionEstablecimientoDto> InformacionEstablecimiento(int establecimiento) 
        {
            return await _unitRepository.clienteInfraestructure.InformacionEstablecimiento(establecimiento);
        }

        public async Task<List<VestimentasEstablecimientosDto>> VestimentasDeEstablecimientos(VestimentasEstablecimientosAggregate requerimientos) 
        {
            return await _unitRepository.clienteInfraestructure.VestimentasDeEstablecimientos(requerimientos);
        }


        public async Task GuardarCarrito(CarritoAggregate requerimientos) 
        {
             await _unitRepository.clienteInfraestructure.GuardarCarrito(requerimientos);
        }

        public async Task<List<ItemsCarrito>> CargarCarrito(int usuarioID) 
        {
            return await _unitRepository.clienteInfraestructure.CargarCarrito(usuarioID);
        }

        public async Task<InformacionDeUnaVestimenta> InformacionDeUnaSolaVestimenta(int vestimentaID)
        {
            return await _unitRepository.clienteInfraestructure.InformacionDeUnaSolaVestimenta(vestimentaID);
        }

        public async Task<ClienteDto> PagoExitoso(int usuarioID)
        {
            return await _unitRepository.clienteInfraestructure.PagoExitoso(usuarioID);
        }

        public async Task<List<ListaPedido>> ListaDeRentas(ListaDePedidoAggregate requerimientos)
        {
            return await _unitRepository.clienteInfraestructure.ListaDeRentas(requerimientos);
        }
    }
}
