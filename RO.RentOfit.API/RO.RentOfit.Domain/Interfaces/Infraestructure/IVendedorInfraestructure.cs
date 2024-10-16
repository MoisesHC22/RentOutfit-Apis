

namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IVendedorInfraestructure
    {
        Task<RespuestaDB> DarDeAltaEstablecimiento(EstablecimientoAggregate registro);
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
        Task<RespuestaDB> RegistrarVestimentas(VestimentaAggregate registro, IFormFile[] Imagenes);
    }
}
