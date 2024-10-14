
namespace RO.RentOfit.Domain.Interfaces.Services
{
    public interface IClientePresenter
    {
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);

        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro, IFormFile Imagen);

        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
    }
}
