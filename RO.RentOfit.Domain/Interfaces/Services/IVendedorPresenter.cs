
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IVendedorPresenter
    {
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
        Task<RetornoEstablecimientoDto> DarDeAltaEstablecimiento(EstablecimientoAggregate registro);
        Task<RespuestaDB> RegistrarVestimentas(VestimentaAggregate registro, IFormFile[] Imagenes);
    }
}
