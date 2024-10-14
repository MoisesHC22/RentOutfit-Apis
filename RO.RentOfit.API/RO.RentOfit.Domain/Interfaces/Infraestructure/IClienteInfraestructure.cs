
namespace RO.RentOfit.Domain.Interfaces.Infraestructure
{
    public interface IClienteInfraestructure
    {
        Task<RespuestaDB> DarDeAltaUnVendedor(int usuarioID);
        Task<IniciarSesionDto> IniciarSesion(IniciarSesionAggregate requerimiento);
        Task<List<ClienteDto>> ObtenerCliente(int usuarioID);
        Task<RespuestaDB> RegistrarCliente(RegistrarClienteAggregate registro, IFormFile Imagen);
    }
}
